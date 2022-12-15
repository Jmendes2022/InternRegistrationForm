CREATE PROCEDURE [dbo].[spReport_MissingSurvey]

AS
BEGIN
	SELECT
		[Id], 
		[FirstName], 
		[LastName], 
		[Department], 
		[DegreePlan], 
		[Role], 
		[StartDate], 
		[EndDate], 
		[DateCreated], 
		[DegreeConcentration], 
		[PersonalEmail], 
		[SchoolEmail], 
		[InternshipEmail], 
		[Track], 
		[InviteToMeetings], 
		[Email], 
		[EntranceSurvey], 
		[WelcomeDocument], 
		[ThreeSignedDocuments], 
		[ESETTraining], 
		[Resume], 
		[Orientation1Access], 
		[Orientation2Access], 
		[TCWAccess], 
		[PlannerAccess], 
		[TeamGroupChat], 
		[OneDriveSetup], 
		[AzureSetup], 
		[ExitSurveySent], 
		[Masterclass], 
		[InternNotes], 
		[LastUpdatedBy], 
		[LastUpdate]
	FROM
		Interns
	WHERE
		EntranceSurvey = 0
		OR
		ExitSurveySent = 0
END