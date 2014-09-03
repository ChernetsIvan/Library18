//using System.Collections.Generic;
//using System.Linq;
//using Library.Data.Infrastructure;
//using Library.Model.Models;
//using Library.Data.Repository;

//namespace Library.Service
//{
//    public interface IUserProfileService
//    {
//        IEnumerable<UserProfile> GetUserProfiles();
//        IEnumerable<UserProfile> GetUserProfiles(string sorting);
//        IEnumerable<UserProfile> GetUserProfiles(int startIndex, int count);
//        IEnumerable<UserProfile> GetUserProfiles(int startIndex, int count, string sorting);
//        UserProfile GetUserProfile(string id);
//        void CreateUserProfile(UserProfile userProfile, string appUserId);
//        void UpdateUserProfile(UserProfile userProfile);
//        void DeleteUserProfile(string id);
//        int GetUserProfileCount();
//        void Sorting(ref IEnumerable<UserProfile> userProfiles, string sorting);
//    }

//    public class UserProfileService : IUserProfileService
//    {
//        private readonly IUserProfileRepository _userProfileRepository;
//        private readonly IUnitOfWork _unitOfWork;

//        public UserProfileService(IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
//        {
//            _userProfileRepository = userProfileRepository;
//            _unitOfWork = unitOfWork;
//        }

//        public IEnumerable<UserProfile> GetUserProfiles()
//        {
//            var userProfiles = _userProfileRepository.GetAll();
//            return userProfiles;
//        }

//        public IEnumerable<UserProfile> GetUserProfiles(string sorting)
//        {
//            var userProfiles = _userProfileRepository.GetAll();
//            Sorting(ref userProfiles, sorting);
//            return userProfiles;
//        }

//        public IEnumerable<UserProfile> GetUserProfiles(int startIndex, int count)
//        {
//            return _userProfileRepository.GetAll().Skip(startIndex).Take(count);           
//        }

//        public IEnumerable<UserProfile> GetUserProfiles(int startIndex, int count, string sorting)
//        {
//            var userProfiles = _userProfileRepository.GetAll().Skip(startIndex).Take(count);
//            Sorting(ref userProfiles, sorting);
//            return userProfiles;
//        }

//        public UserProfile GetUserProfile(string id)
//        {
//            var userProfile = _userProfileRepository.GetById(id);
//            return userProfile;
//        }

//        public void CreateUserProfile(UserProfile userProfile, string appUserId)
//        {
//            userProfile.UserProfileId = appUserId;       
//            _userProfileRepository.Add(userProfile);
//            SaveChanges();
//        }
//        public void UpdateUserProfile(UserProfile userProfile)
//        {
//            _userProfileRepository.Update(userProfile);
//            SaveChanges();
//        }

//        public void DeleteUserProfile(string id)
//        {
//            var userProfile = _userProfileRepository.GetById(id);
//            _userProfileRepository.Delete(userProfile);
//            SaveChanges();
//        }

//        public int GetUserProfileCount()
//        {
//            return _userProfileRepository.GetAll().Count();
//        }
//        private void SaveChanges()
//        {
//            _unitOfWork.Commit();
//        }

//        public void Sorting(ref IEnumerable<UserProfile> userProfiles, string sorting)
//        {
//            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
//            {
//                userProfiles = userProfiles.OrderBy(p => p.Name);
//            }
//            else if (sorting.Equals("Name DESC"))
//            {
//                userProfiles = userProfiles.OrderByDescending(p => p.Name);
//            }
//            else if (sorting.Equals("LastName ASC"))
//            {
//                userProfiles = userProfiles.OrderBy(p => p.LastName);
//            }
//            else if (sorting.Equals("LastName DESC"))
//            {
//                userProfiles = userProfiles.OrderByDescending(p => p.LastName);
//            }
//            else if (sorting.Equals("Floor ASC"))
//            {
//                userProfiles = userProfiles.OrderBy(p => p.Floor);
//            }
//            else if (sorting.Equals("Floor DESC"))
//            {
//                userProfiles = userProfiles.OrderByDescending(p => p.Floor);
//            }
//            else if (sorting.Equals("Gender ASC"))
//            {
//                userProfiles = userProfiles.OrderBy(p => p.Gender);
//            }
//            else if (sorting.Equals("Gender DESC"))
//            {
//                userProfiles = userProfiles.OrderByDescending(p => p.Gender);
//            }
//            else if (sorting.Equals("Mail ASC"))
//            {
//                userProfiles = userProfiles.OrderBy(p => p.Mail);
//            }
//            else if (sorting.Equals("Mail DESC"))
//            {
//                userProfiles = userProfiles.OrderByDescending(p => p.Mail);
//            }
//            else if (sorting.Equals("Phone ASC"))
//            {
//                userProfiles = userProfiles.OrderBy(p => p.Phone);
//            }
//            else if (sorting.Equals("Phone DESC"))
//            {
//                userProfiles = userProfiles.OrderByDescending(p => p.Phone);
//            }
//            else if (sorting.Equals("PlaceDescription ASC"))
//            {
//                userProfiles = userProfiles.OrderBy(p => p.PlaceDescription);
//            }
//            else if (sorting.Equals("PlaceDescription DESC"))
//            {
//                userProfiles = userProfiles.OrderByDescending(p => p.PlaceDescription);
//            }
//            else if (sorting.Equals("Room ASC"))
//            {
//                userProfiles = userProfiles.OrderBy(p => p.Room);
//            }
//            else if (sorting.Equals("Room DESC"))
//            {
//                userProfiles = userProfiles.OrderByDescending(p => p.Room);
//            }
//            else if (sorting.Equals("Skype ASC"))
//            {
//                userProfiles = userProfiles.OrderBy(p => p.Skype);
//            }
//            else if (sorting.Equals("Skype DESC"))
//            {
//                userProfiles = userProfiles.OrderByDescending(p => p.Skype);
//            }
//        }
//    }
//}
