using CMS.DAL;

namespace CMS.BLL;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    public DoctorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // 🔥 Get All
    public async Task<IEnumerable<DoctorDto>> GetAllAsync(Query query)
    {
        var doctors = await _unitOfWork.Doctors.GetAllAsync(
            query,
            d => d.Appointments
        );

        var result = doctors.Select(d => new DoctorDto
        {
            Id = d.Id,
            Name = d.Name,
            AppointmentsCount = d.Appointments.Count
        });

        return result;
    }

    // 🔥 Get By Id
    public async Task<DoctorDto?> GetByIdAsync(Guid id)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(
            id,
            d => d.Appointments
        );

        if (doctor == null)
            return null;

        return new DoctorDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            AppointmentsCount = doctor.Appointments.Count
        };
    }

    // 🔥 Add
    public async Task<CommonResponse> AddAsync(CreateDoctorDto dto)
    {
        var doctor = new Doctor
        {
            Id = Guid.NewGuid(),

            Name = dto.Name
        };

        await _unitOfWork.Doctors.AddAsync(doctor);

        await _unitOfWork.SaveChangesAsync();

        return new CommonResponse("added successfully", true);
    }

    // 🔥 Update
    public async Task<CommonResponse> UpdateAsync(Guid id, UpdateDoctorDto dto)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

        if (doctor == null)
            return new CommonResponse("not found", false);

        doctor.Name = dto.Name;

        _unitOfWork.Doctors.Update(doctor);

        await _unitOfWork.SaveChangesAsync();
        return new CommonResponse("updated", true);
    }

    // 🔥 Delete
    public async Task<CommonResponse> DeleteAsync(Guid id)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

        if (doctor == null)
            return new CommonResponse("not found", false); ;

        _unitOfWork.Doctors.Delete(doctor);

        await _unitOfWork.SaveChangesAsync();
        return new CommonResponse("Deleted", true);
    }
}

