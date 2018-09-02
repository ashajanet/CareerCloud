using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.WCF
{
    [ServiceContract]
    interface IApplicant
    {
        //ApplicantEducationPoco
        [OperationContract]
        void AddApplicantEducation(ApplicantEducationPoco[] pocos);
        [OperationContract]
        List<ApplicantEducationPoco> GetAllApplicantEducation();
        [OperationContract]
        ApplicantEducationPoco GetSingleApplicantEducation(string Id);
        [OperationContract]
        void RemoveApplicantEducation(ApplicantEducationPoco[] pocos);
        [OperationContract]
        void UpdateApplicantEducation(ApplicantEducationPoco[] pocos);
        //ApplicantJobApplicationPoco
        [OperationContract]
        void AddApplicantJobApplication(ApplicantJobApplicationPoco[] pocos);
        [OperationContract]
        List<ApplicantJobApplicationPoco> GetAllApplicantJobApplication();
        [OperationContract]
        ApplicantJobApplicationPoco GetSingleApplicantJobApplication(string Id);
        [OperationContract]
        void RemoveApplicantJobApplication(ApplicantJobApplicationPoco[] pocos);
        [OperationContract]
        void UpdateApplicantJobApplication(ApplicantJobApplicationPoco[] pocos);
        //ApplicantProfilePoco
        [OperationContract]
        void AddApplicantProfile(ApplicantProfilePoco[] pocos);
        [OperationContract]
        List<ApplicantProfilePoco> GetAllApplicantProfile();
        [OperationContract]
        ApplicantProfilePoco GetSingleApplicantProfile(string Id);
        [OperationContract]
        void RemoveApplicantProfile(ApplicantProfilePoco[] pocos);
        [OperationContract]
        void UpdateApplicantProfile(ApplicantProfilePoco[] pocos);
        //ApplicantResumePoco
        [OperationContract]
        void AddApplicantResume(ApplicantResumePoco[] pocos);
        [OperationContract]
        List<ApplicantResumePoco> GetAllApplicantResume();
        [OperationContract]
        ApplicantResumePoco GetSingleApplicantResume(string Id);
        [OperationContract]
        void RemoveApplicantResume(ApplicantResumePoco[] pocos);
        [OperationContract]
        void UpdateApplicantResume(ApplicantResumePoco[] pocos);
        // ApplicantSkillPoco
        [OperationContract]
        void AddApplicantSkill(ApplicantSkillPoco[] pocos);
        [OperationContract]
        List<ApplicantSkillPoco> GetAllApplicantSkill();
        [OperationContract]
        ApplicantSkillPoco GetSingleApplicantSkill(string Id);
        [OperationContract]
        void RemoveApplicantSkill(ApplicantSkillPoco[] pocos);
        [OperationContract]
        void UpdateApplicantSkill(ApplicantSkillPoco[] pocos);
        //ApplicantWorkHistoryPoco
        [OperationContract]
        void AddApplicantWorkHistory(ApplicantWorkHistoryPoco[] pocos);
        [OperationContract]
        List<ApplicantWorkHistoryPoco> GetAllApplicantWorkHistory();
        [OperationContract]
        ApplicantWorkHistoryPoco GetSingleApplicantWorkHistory(string Id);
        [OperationContract]
        void RemoveApplicantWorkHistory(ApplicantWorkHistoryPoco[] pocos);
        [OperationContract]
        void UpdateApplicantWorkHistory(ApplicantWorkHistoryPoco[] pocos);

        }
}
