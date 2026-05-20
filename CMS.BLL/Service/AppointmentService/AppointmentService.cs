using CMS.DAL;

namespace CMS.BLL;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;
    public AppointmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // 🔥 Get All
    public async Task<IEnumerable<AppointmentDto>> GetAllAsync(Query query)
    {
        var appointments = await _unitOfWork.Appointments.GetAllAsync(
            query,
            a => a.Doctor,
            a => a.Patient
        );

        var result = appointments.Select(a => new AppointmentDto
        {
            Id = a.Id,
            Date = a.Date,

            DoctorId = a.DoctorId,
            DoctorName = a.Doctor.Name,

            PatientId = a.PatientId,
            PatientName = a.Patient.Name
        });

        return result;
    }

    // 🔥 Get By Id
    public async Task<AppointmentDto?> GetByIdAsync(Guid id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(
            id,
            a => a.Doctor,
            a => a.Patient
        );

        if (appointment == null)
            return null;

        return new AppointmentDto
        {
            Id = appointment.Id,
            Date = appointment.Date,

            DoctorId = appointment.DoctorId,
            DoctorName = appointment.Doctor?.Name,

            PatientId = appointment.PatientId,
            PatientName = appointment.Patient?.Name
        };
    }

    // 🔥 Add
    public async Task<CommonResponse> AddAsync(CreateAppointmentDto dto)
    {
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),

            Date = dto.Date,
            DoctorId = dto.DoctorId,
            PatientId = dto.PatientId
        };

        await _unitOfWork.Appointments.AddAsync(appointment);

        await _unitOfWork.SaveChangesAsync();
        return new CommonResponse("Added successfully", true);
    }

    // 🔥 Update
    public async Task<CommonResponse> UpdateAsync(Guid id, UpdateAppointmentDto dto)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);

        if (appointment == null)
            return new CommonResponse("not found", false);

        appointment.Date = dto.Date;
        appointment.DoctorId = dto.DoctorId;
        appointment.PatientId = dto.PatientId;

        _unitOfWork.Appointments.Update(appointment);

        await _unitOfWork.SaveChangesAsync();
        return new CommonResponse("updated", true);
    }

    // 🔥 Delete
    public async Task<CommonResponse> DeleteAsync(Guid id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);

        if (appointment == null)
            return new CommonResponse("not found", false);

        _unitOfWork.Appointments.Delete(appointment);

        await _unitOfWork.SaveChangesAsync();
        return new CommonResponse("deleted", true);

    }
}

