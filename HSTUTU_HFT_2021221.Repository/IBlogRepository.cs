﻿using HSTUTU_HFT_2021221.Models;

namespace HSTUTU_HFT_2021221.Repository
{
    public interface IBlogRepository : IReposotiry<Blog>
    {
        void Update(Blog blog);
    }
}
