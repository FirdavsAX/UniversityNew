﻿using University.Models.InstructorViewModels;

namespace University.Services
{
    public interface IInstructorService
    {
        Task<InstructorDisplayViewModel> Create(InstructorActionViewModel instructor);
        Task<InstructorActionViewModel> Update(InstructorActionViewModel instructor);
        Task<InstructorDisplayViewModel> GetById(int id);
        Task<IEnumerable<InstructorDisplayViewModel>> GetInstructors(
            int? departmentId = 0,
            string searchString = "",
            string sortOrder = "name_asc"
            );
        Task Delete(int id);
    }
}
