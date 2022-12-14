using FluentValidation;

namespace NMMS.Application.CQRSDistributor.Commands.CreateDistributor
{
    public class CreateDistributorCommandsValidator : AbstractValidator<CreateDistributorCommands>
    {
        public CreateDistributorCommandsValidator()
        {
            #region DistributorModel
            RuleFor(request => request.DistributorModel.FirstName)
                .NotNull()
                .NotEmpty()
                .Must(x => x.Length > 0 && x.Length <= 50);
            RuleFor(request => request.DistributorModel.LastName)
                .NotNull()
                .NotEmpty()
                .Must(x => x.Length > 0 && x.Length <= 50);
            RuleFor(request => request.DistributorModel.SexTypeId)
                .NotNull()
                .NotEmpty();
            RuleFor(request => request.DistributorModel.BirthDate)
                .NotNull()
                .NotEmpty();
            RuleFor(request => request.DistributorModel.FileId)
                .NotNull()
                .NotEmpty();
            #endregion

            #region IdentificationInformationModel
            RuleFor(request => request.IdentificationInformationModel.DocumentTypeId)
                .NotNull()
                .NotEmpty();
            RuleFor(request => request.IdentificationInformationModel.DocumentSeries)
                .Must(x => x.Length > 0 && x.Length <= 10);
            RuleFor(request => request.IdentificationInformationModel.DocumentNumber)
                .Must(x => x.Length > 0 && x.Length <= 10);
            RuleFor(request => request.IdentificationInformationModel.ReleaseDate)
                .NotNull()
                .NotEmpty();
            RuleFor(request => request.IdentificationInformationModel.DocumentTerms)
                .NotNull()
                .NotEmpty();
            RuleFor(request => request.IdentificationInformationModel.IdentityNumber)
                .NotNull()
                .NotEmpty()
                .Must(x => x.Length > 0 && x.Length <= 50);
            RuleFor(request => request.IdentificationInformationModel.IssuingCompany)
                .Must(x => x.Length > 0 && x.Length <= 100);
            #endregion

            #region AddressModel
            RuleFor(request => request.AddressModel.AddressTypeId)
                .NotNull()
                .NotEmpty();
            RuleFor(request => request.AddressModel.AddressInfo)
                .NotNull()
                .NotEmpty()
                .Must(x => x.Length > 0 && x.Length <= 100);
            #endregion

            #region ContactModel
            RuleFor(request => request.ContactModel.ContactTypeId)
                .NotNull()
                .NotEmpty();
            RuleFor(request => request.ContactModel.ContactInformation)
                .NotNull()
                .NotEmpty()
                .Must(x => x.Length > 0 && x.Length <= 100);
            #endregion
        }
    }
}
