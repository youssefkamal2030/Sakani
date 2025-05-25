using AutoMapper;
using Sakani.Data.Models;
using Sakani.DA.DTOs;

namespace Sakani.DA.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            // Student mappings
            CreateMap<Student, StudentDto>();
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();

            // Owner mappings
            CreateMap<Owner, OwnerDto>();
            CreateMap<CreateOwnerDto, Owner>();
            CreateMap<UpdateOwnerDto, Owner>();

            // Apartment mappings
            CreateMap<Apartment, ApartmentDto>()
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));
            CreateMap<CreateApartmentDto, Apartment>();
            CreateMap<UpdateApartmentDto, Apartment>();

            // Room mappings
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.Beds, opt => opt.MapFrom(src => src.Beds));
            CreateMap<CreateRoomDto, Room>();
            CreateMap<UpdateRoomDto, Room>();

            // Bed mappings
            CreateMap<Bed, BedDto>();
            CreateMap<CreateBedDto, Bed>();
            CreateMap<UpdateBedDto, Bed>();

            // Booking mappings
            CreateMap<Booking, BookingDto>();
            CreateMap<CreateBookingDto, Booking>();
            CreateMap<UpdateBookingDto, Booking>();

            // Feedback mappings
            CreateMap<Feedback, FeedbackDto>();
            CreateMap<CreateFeedbackDto, Feedback>();
            CreateMap<UpdateFeedbackDto, Feedback>();
        }
    }
} 