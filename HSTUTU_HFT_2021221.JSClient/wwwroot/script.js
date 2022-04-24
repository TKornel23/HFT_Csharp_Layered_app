let blogs = [];

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
}

function display() {
    document.getElementById("results").innerHTML = "";
    blogs.forEach(t => {
        document.getElementById("results").innerHTML += "<tr><td>" + t.id + " </td><td>" + t.title + `</td><td> <button class="btn btn-danger" onclick="remove(${t.id})">Delete</button></td><td> <button class="btn btn-success" onclick="setUpdate('${String(t.title)}', ${t.id})">Update</button></td></tr>`;
    });
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

function remove(id) {
    fetch("http://localhost:57125/blog/" + id, {
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