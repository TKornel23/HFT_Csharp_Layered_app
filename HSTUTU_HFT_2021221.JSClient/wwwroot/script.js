let blogs = [];
let posts = [];
let tags = [];

let connection = null;

getData();
setup();

function setup() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:57125/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BlogCreated", (user, message) => { getData(); });
    connection.on("BlogDeleted", (user, message) => { getData(); });
    connection.on("BlogUpdated", (user, message) => { getData(); });

    connection.on("PostCreated", (user, message) => { getData(); });
    connection.on("PostDeleted", (user, message) => { getData(); });
    connection.on("PostUpdated", (user, message) => { getData(); });

    connection.on("TagCreated", (user, message) => { getData(); });
    connection.on("TagDeleted", (user, message) => { getData(); });
    connection.on("TagUpdated", (user, message) => { getData(); });

    connection.onclose
        (async () => {
            await start();
        });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getData() {
    await fetch("http://localhost:57125/blog")
        .then(x => x.json())
        .then(y => {
            blogs = y;
            display();
        });


    await fetch("http://localhost:57125/post")
        .then(x => x.json())
        .then(y => {
            posts = y;
            display();
        });

    await fetch("http://localhost:57125/tag")
        .then(x => x.json())
        .then(y => {
            tags = y;
            display();
        });

    while (document.getElementById('blog-options').hasChildNodes()) {
        document.getElementById('blog-options').removeChild(document.getElementById('blog-options').firstChild);
    }


    while (document.getElementById('post-options').hasChildNodes()) {
        document.getElementById('post-options').removeChild(document.getElementById('post-options').firstChild);
    }

    for (var i = 0; i < blogs.length; i++) {
        var option = document.createElement("option");
        option.text = blogs[i].id;
        option.value = blogs[i].id;

        var select = document.getElementById('blog-options');
        select.appendChild(option);
    }

    for (var i = 0; i < posts.length; i++) {
        var option = document.createElement("option");
        option.text = posts[i].id;
        option.value = posts[i].id;

        var select = document.getElementById('post-options');
        select.appendChild(option);
    }
}

function display() {
    document.getElementById("results").innerHTML = "";
    blogs.forEach(t => {
        document.getElementById("results").innerHTML += "<tr><td>" + t.id + " </td><td>" + t.title + `</td><td> <button class="btn btn-danger" onclick="remove(${t.id}, 'blog')">Delete</button></td><td> <button class="btn btn-success" onclick="setUpdate('${String(t.title)}', ${t.id})">Update</button></td></tr>`;
    });

    document.getElementById("results-post").innerHTML = "";
    posts.forEach(p => {
        document.getElementById("results-post").innerHTML += "<tr><td>" + p.id + "</td><td>" + p.title + "</td><td>" + p.blogId + "</td><td>" + p.postContent + "</td><td>" + p.likes + `</td><td> <button class="btn btn-danger" onclick="remove(${p.id}, 'post')">Delete</button></td><td> <button class="btn btn-success" onclick="setUpdatePost(${p.id}, '${p.title}',${p.blogId}, '${p.postContent}', ${p.likes})">Update</button></td></tr>`;
    })

    document.getElementById("results-tag").innerHTML = "";
    tags.forEach(t => {
        document.getElementById("results-tag").innerHTML += "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.postId + `</td><td> <button class="btn btn-danger" onclick="remove(${t.id}, 'tag')">Delete</button></td><td> <button class="btn btn-success" onclick="setUpdateTag(${t.id}, '${t.name}', ${t.postId})">Update</button></td></tr>`;
    })
}

function setUpdateTag(id, name, postId) {
    document.getElementById('edit-name-tag').value = name;
    document.getElementById('edit-id-tag').value = id;
    document.getElementById('edit-postId-tag').value = postId;
}

function UpdateTag() {
    var name = document.getElementById('edit-name-tag').value;
    var id = document.getElementById('edit-id-tag').value;
    var postId = document.getElementById('edit-postId-tag').value;

    fetch("http://localhost:57125/tag/", {
        method: "PUT",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: id, name: name, postId: postId }),
    })
        .then(response => response)
        .then(data => {
            getData();
        })
        .catch((error) => { console.log('Error: ', error) });

    document.getElementById('edit-name-tag').value = "";
    document.getElementById('edit-id-tag').value = "";
    document.getElementById('edit-postId-tag').value = "";
}

function createTag() {
    let name = document.getElementById("tag-name").value;
    select = document.getElementById('blog-options');
    var postId = select.options[select.selectedIndex].value;

    fetch("http://localhost:57125/tag", {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: name, postId: postId }),
    })
        .then(response => response)
        .then(data => {
            getData();
        })
        .catch((error) => { console.log('Error: ', error) });
    document.getElementById("tag-name").value = "";
    document.getElementById("tag-postId").value = "";
}

function setUpdatePost(id, title, blogId, Postcontent, Likes) {
    document.getElementById('edit-id-post').value = id;
    document.getElementById('edit-title-post').value = title;
    document.getElementById('edit-blogId-post').value = blogId;
    document.getElementById('edit-postContent-post').value = Postcontent;
    document.getElementById('edit-likes-post').value = Likes;
}

function UpdatePost() {
    var id = document.getElementById('edit-id-post');
    var title = document.getElementById('edit-title-post');
    var blogId = document.getElementById('edit-blogId-post');
    var content = document.getElementById('edit-postContent-post');
    var likes = document.getElementById('edit-likes-post');

    fetch("http://localhost:57125/post/", {
        method: "PUT",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: id.value, title: title.value, blogId: blogId.value, postContent: content.value, likes: likes.value }),
    })
        .then(response => response)
        .then(data => {
            getData();
        })
        .catch((error) => { console.log('Error: ', error) });
    id.value = "";
    title.value = "";
    blogId.value = "";
    content.value = "";
    likes.value = "";
}

function create() {
    let name = document.getElementById("blogName").value;
    fetch("http://localhost:57125/blog", {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: name }),
    })
        .then(response => response)
        .then(data => {
            getData();
        })
        .catch((error) => { console.log('Error: ', error) });
    document.getElementById("blogName").value = "";
}

function remove(id, type) {
    fetch("http://localhost:57125/" + type + "/" + id, {
        method: "DELETE",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: name }),
    })
        .then(response => response)
        .then(data => {
            getData();
        })
        .catch((error) => { console.log('Error: ', error) });
}

function setUpdate(title, id) {
    document.getElementById('edit-id').value = id;
    document.getElementById('edit-title').value = title;
}

function Update() {
    id = document.getElementById('edit-id').value;
    title = document.getElementById('edit-title').value;

    fetch("http://localhost:57125/blog/", {
        method: "PUT",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: id, title: title }),
    })
        .then(response => response)
        .then(data => {
            getData();
        })
        .catch((error) => { console.log('Error: ', error) });

    document.getElementById('edit-id').value = "";
    document.getElementById('edit-title').value = "";
}

function createPost() {
    var title = document.getElementById('post-title');
    select = document.getElementById('blog-options');
    var blogId = select.options[select.selectedIndex];
    var content = document.getElementById('post-content');
    var likes = document.getElementById('post-likes');

    fetch("http://localhost:57125/post", {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: title.value, blogId: blogId.value, postContent: content.value, likes: likes.value }),
    })
        .then(response => response)
        .then(data => {
            getData();
        })
        .catch((error) => { console.log('Error: ', error) });
    title.value = "";
    blogId.value = "";
    connection.value = "";
    likes.value = "";
}