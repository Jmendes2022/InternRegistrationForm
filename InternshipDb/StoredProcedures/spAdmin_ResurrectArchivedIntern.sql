CREATE PROCEDURE [dbo].[spAdmin_ResurrectArchivedIntern]
	@Id int
AS
BEGIN
	INSERT INTO Interns
	(
		FirstName,
		LastName,
		Department, 
		DegreePlan,
		[Role],
		StartDate,
		EndDate, 
		DateCreated,
		DegreeConcentration,
		PersonalEmail,
		SchoolEmail,
		InternshipEmail,
		Track,
		InviteToMeetings,
		Email,
		EntranceSurvey, 
		WelcomeDocument, 
		ThreeSignedDocuments,
		ESETTraining,
		[Resume],
		Orientation1Access,
		Orientation2Access,
		TCWAccess,
		PlannerAccess, 
		TeamGroupChat,
		OneDriveSetup, 
		AzureSetup, 
		ExitSurveySent,
		Masterclass,
		InternNotes, 
		LastUpdate
	)
	SELECT
		FirstName,
		LastName,
		Department, 
		DegreePlan,
		[Role],
		StartDate,
		EndDate, 
		DateCreated,
		DegreeConcentration,
		PersonalEmail,
		SchoolEmail,
		InternshipEmail,
		Track,
		InviteToMeetings,
		Email,
		EntranceSurvey, 
		WelcomeDocument, 
		ThreeSignedDocuments,
		ESETTraining,
		[Resume],
		Orientation1Access,
		Orientation2Access,
		TCWAccess,
		PlannerAccess, 
		TeamGroupChat,
		OneDriveSetup, 
		AzureSetup, 
		ExitSurveySent,
		Masterclass,
		InternNotes, 
		LastUpdate
	FROM
		ArchivedInterns
	WHERE
		Id = Id
	DELETE FROM
		ArchivedInterns
	WHERE
		ID = @Id
	
	SELECT SCOPE_IDENTITY();
END