CREATE PROCEDURE [dbo].[spIntern_ResurrectIntern]
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
		LastUpdatedBy,
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
		LastUpdatedBy,
		LastUpdate
	FROM
		DroppedInterns
	WHERE
		Id = @Id;

	DELETE FROM
		DroppedInterns
	WHERE
		Id = @Id
	SELECT SCOPE_IDENTITY();
END