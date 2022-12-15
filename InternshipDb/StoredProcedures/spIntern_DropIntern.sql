CREATE PROCEDURE [dbo].[spIntern_DropIntern]
	@id int

AS
BEGIN
	INSERT INTO DroppedInterns 
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
		Interns
	WHERE
		Id = @id;
	
	DELETE FROM
		Interns
	WHERE
		Id = @id;
END