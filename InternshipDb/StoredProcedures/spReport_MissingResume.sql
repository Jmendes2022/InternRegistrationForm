﻿CREATE PROCEDURE [dbo].[spReport_MissingResume]


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
		[Resume] = 0;
END