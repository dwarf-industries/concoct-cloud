CREATE TABLE IF NOT EXISTS UserNotes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DateOfMessage DATETIME
);CREATE TABLE IF NOT EXISTS AssociatedAccountNotes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NoteId INT NOT NULL,
    UserAccountId INT NOT NULL,
    ProjectId INT NOT NULL
);CREATE TABLE IF NOT EXISTS PublicMessage (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    WorkItemRelationid INT,
    Rating INT,
    DateOfMessage DATETIME
);CREATE TABLE IF NOT EXISTS AssociatedProjectBoards (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    BoardId INT,
    ProjectId INT,
    Position INT
);CREATE TABLE IF NOT EXISTS AssociatedProjectPublicMessages (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    MessageId INT NOT NULL,
    ProjectId INT NOT NULL
);CREATE TABLE IF NOT EXISTS Notifications (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    WorkItemRelationid INT,
    DateOfMessage DATETIME,
    NotificationType INT
);CREATE TABLE IF NOT EXISTS NotificationTypes (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS Risks (
    Id INT AUTO_INCREMENT PRIMARY KEY NOT NULL
);CREATE TABLE IF NOT EXISTS AssociatedProjectNotifications (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NotificationId INT NOT NULL,
    ProjectId INT NOT NULL,
    NewNotification INT,
    UserAccountId INT
);CREATE TABLE IF NOT EXISTS Efforts (
    Id INT NOT NULL
);CREATE TABLE IF NOT EXISTS AssociatedUserNotifications (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NotificationId INT NOT NULL,
    UserId INT NOT NULL,
    NewNotification INT
);CREATE TABLE IF NOT EXISTS ValueAreas (
    Id INT AUTO_INCREMENT PRIMARY KEY NOT NULL
);CREATE TABLE IF NOT EXISTS AssociatedWrorkItemChildren (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    WorkItemId INT,
    WorkItemChildId INT,
    RelationType INT
);CREATE TABLE IF NOT EXISTS PublicMessages (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DateOfMessage DATETIME
);CREATE TABLE IF NOT EXISTS AssociatedProjectPublicDiscussions (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProjectId INT,
    PublicMessageId INT
);CREATE TABLE IF NOT EXISTS Commits (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DateOfCommit DATETIME
);CREATE TABLE IF NOT EXISTS Branches (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProjectId INT NOT NULL,
    DateOfCreation DATETIME
);CREATE TABLE IF NOT EXISTS SystemFiles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DateOfMessage DATETIME,
    FileType INT
);CREATE TABLE IF NOT EXISTS AssociatedBranchCommits (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CommitId INT NOT NULL,
    BranchId INT NOT NULL
);CREATE TABLE IF NOT EXISTS AssociatedWorkItemFiles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    WorkItemId INT,
    FileId INT
);CREATE TABLE IF NOT EXISTS WorkItemRealtionshipType (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS AssociatedProjectFeedback (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProjectId INT,
    MessageId INT,
    rating INT
);CREATE TABLE IF NOT EXISTS UserAccounts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CreationDate DATETIME NOT NULL,
    ProjectRights INT
);CREATE TABLE IF NOT EXISTS AssociatedRepositoryBranches (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    RepositoryId INT NOT NULL,
    BranchId INT NOT NULL
);CREATE TABLE IF NOT EXISTS ApiKeys (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS WorkItemTypes (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS AssociatedProjectApiKeys (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProjectId INT,
    KeyId INT
);CREATE TABLE IF NOT EXISTS WorkItem (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    WorkItemTypeId INT,
    AssignedAccount INT,
    StateId INT,
    AreaId INT,
    StartDate DATETIME,
    EndDate DATETIME,
    PriorityId INT,
    ClassificationId INT,
    DevelopmentId INT,
    ParentId INT,
    Reason INT,
    Iteration INT,
    itemPriority INT,
    Severity INT,
    Activity INT,
    BranchId INT,
    FoundInBuild INT,
    IntegratedInBuild INT,
    ReasonId INT,
    RelationId INT,
    RiskId INT,
    ValueAreaId INT,
    DueDate DATETIME,
    IsPublic INT
);CREATE TABLE IF NOT EXISTS Projects (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    RepositoryId INT NOT NULL,
    CreationDate DATETIME,
    BoardId INT,
    PublicBoard INT,
    AllowPublicControl INT,
    AllowPublicFeatures INT,
    AllowPublicBugs INT,
    AllowPublicFeedback INT,
    AllowPublicMessages INT
);CREATE TABLE IF NOT EXISTS FileTypes (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS sysdiagrams (
    principal_id INT NOT NULL,
    diagram_id INT AUTO_INCREMENT PRIMARY KEY,
    version INT
);CREATE TABLE IF NOT EXISTS Repository (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS Files (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DateOfFile DATETIME
);CREATE TABLE IF NOT EXISTS AssociatedProjectIterations (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProjectId INT NOT NULL,
    IterationId INT NOT NULL
);CREATE TABLE IF NOT EXISTS AssociatedCommitFiles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FileId INT,
    CommitId INT,
    DateOfCommit DATETIME
);CREATE TABLE IF NOT EXISTS Boards (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    RepositoryId INT NOT NULL,
    BoardType INT NOT NULL
);CREATE TABLE IF NOT EXISTS UserRights (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    WorkItemRule INT NOT NULL,
    ChatChannelsRule INT NOT NULL,
    UpdateUserRights INT NOT NULL,
    ManageIterations INT NOT NULL,
    ManageUserdays INT NOT NULL,
    ViewOtherPeoplesWork INT NOT NULL
);CREATE TABLE IF NOT EXISTS AssociatedBoardWorkItems (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    BoardId INT NOT NULL,
    WorkItemId INT NOT NULL,
    ProjectId INT
);CREATE TABLE IF NOT EXISTS AssociatedProjectMemberRights (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    RightsId INT,
    UserAccountId INT,
    ProjectId INT
);CREATE TABLE IF NOT EXISTS AssociatedProjectMembers (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserAccountId INT NOT NULL,
    ProjectId INT NOT NULL,
    RepositoryId INT NOT NULL
);CREATE TABLE IF NOT EXISTS WorkItemStates (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS WorkItemAreas (
    ID INT AUTO_INCREMENT PRIMARY KEY NOT NULL
);CREATE TABLE IF NOT EXISTS Changelogs (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DayOfLog INT
);CREATE TABLE IF NOT EXISTS AssociatedProjectChangelogs (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    LogId INT,
    ProjectId INT,
    CurrentMonth INT,
    LogYear INT
);CREATE TABLE IF NOT EXISTS WorkItemPriorities (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS WorkItemSeverities (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS AssociatedWorkItemChangelogs (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    LogId INT,
    workitemId INT,
    ProjectId INT
);CREATE TABLE IF NOT EXISTS WorkItemActivity (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS WorkItemIterations (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    IsActive INT
);CREATE TABLE IF NOT EXISTS WorkItemReasons (
    Id INT AUTO_INCREMENT PRIMARY KEY
);CREATE TABLE IF NOT EXISTS Builds (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FrameworkVersion INT,
    DateOfBuild DATETIME,
    CompleationStatus INT,
    AccountId INT,
    PlatformId INT
);CREATE TABLE IF NOT EXISTS WorkItemMessage (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    SenderId INT NOT NULL,
    DateOfMessage DATETIME
);CREATE TABLE IF NOT EXISTS AssociatedProjectBuilds (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    RepositoryId INT,
    BuildId INT,
    ProjectId INT
);CREATE TABLE IF NOT EXISTS AssociatedWorkItemMessages (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    WorkItemId INT NOT NULL,
    MessageId INT NOT NULL
);CREATE TABLE IF NOT EXISTS WorkItemRelations (
    Id INT AUTO_INCREMENT PRIMARY KEY
);
ALTER TABLE AssociatedAccountNotes ADD FOREIGN KEY (NoteId) REFERENCES UserNotes(Id);
ALTER TABLE AssociatedAccountNotes ADD FOREIGN KEY (UserAccountId) REFERENCES UserAccounts(Id);
ALTER TABLE AssociatedAccountNotes ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedBoardWorkItems ADD FOREIGN KEY (BoardId) REFERENCES Boards(Id);
ALTER TABLE AssociatedBoardWorkItems ADD FOREIGN KEY (WorkItemId) REFERENCES WorkItem(Id);
ALTER TABLE AssociatedBoardWorkItems ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedBranchCommits ADD FOREIGN KEY (CommitId) REFERENCES Commits(Id);
ALTER TABLE AssociatedBranchCommits ADD FOREIGN KEY (BranchId) REFERENCES Branches(Id);
ALTER TABLE AssociatedCommitFiles ADD FOREIGN KEY (FileId) REFERENCES Files(Id);
ALTER TABLE AssociatedCommitFiles ADD FOREIGN KEY (CommitId) REFERENCES Commits(Id);
ALTER TABLE AssociatedProjectApiKeys ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedProjectApiKeys ADD FOREIGN KEY (KeyId) REFERENCES ApiKeys(Id);
ALTER TABLE AssociatedProjectBoards ADD FOREIGN KEY (BoardId) REFERENCES Boards(Id);
ALTER TABLE AssociatedProjectBoards ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedProjectBuilds ADD FOREIGN KEY (RepositoryId) REFERENCES Repository(Id);
ALTER TABLE AssociatedProjectBuilds ADD FOREIGN KEY (BuildId) REFERENCES Builds(Id);
ALTER TABLE AssociatedProjectBuilds ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedProjectChangelogs ADD FOREIGN KEY (LogId) REFERENCES Changelogs(Id);
ALTER TABLE AssociatedProjectChangelogs ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedProjectFeedback ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedProjectFeedback ADD FOREIGN KEY (MessageId) REFERENCES PublicMessages(Id);
ALTER TABLE AssociatedProjectIterations ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedProjectIterations ADD FOREIGN KEY (IterationId) REFERENCES WorkItemIterations(Id);
ALTER TABLE AssociatedProjectMemberRights ADD FOREIGN KEY (RightsId) REFERENCES UserRights(Id);
ALTER TABLE AssociatedProjectMemberRights ADD FOREIGN KEY (UserAccountId) REFERENCES UserAccounts(Id);
ALTER TABLE AssociatedProjectMemberRights ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedProjectMembers ADD FOREIGN KEY (UserAccountId) REFERENCES UserAccounts(Id);
ALTER TABLE AssociatedProjectMembers ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedProjectMembers ADD FOREIGN KEY (RepositoryId) REFERENCES Repository(Id);
ALTER TABLE AssociatedProjectNotifications ADD FOREIGN KEY (NotificationId) REFERENCES Notifications(Id);
ALTER TABLE AssociatedProjectNotifications ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedProjectNotifications ADD FOREIGN KEY (UserAccountId) REFERENCES UserAccounts(Id);
ALTER TABLE AssociatedProjectPublicDiscussions ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedProjectPublicDiscussions ADD FOREIGN KEY (PublicMessageId) REFERENCES PublicMessages(Id);
ALTER TABLE AssociatedProjectPublicMessages ADD FOREIGN KEY (MessageId) REFERENCES PublicMessage(Id);
ALTER TABLE AssociatedProjectPublicMessages ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedRepositoryBranches ADD FOREIGN KEY (RepositoryId) REFERENCES Repository(Id);
ALTER TABLE AssociatedRepositoryBranches ADD FOREIGN KEY (BranchId) REFERENCES Branches(Id);
ALTER TABLE AssociatedUserNotifications ADD FOREIGN KEY (NotificationId) REFERENCES Notifications(Id);
ALTER TABLE AssociatedUserNotifications ADD FOREIGN KEY (UserId) REFERENCES UserAccounts(Id);
ALTER TABLE AssociatedWorkItemChangelogs ADD FOREIGN KEY (LogId) REFERENCES Changelogs(Id);
ALTER TABLE AssociatedWorkItemChangelogs ADD FOREIGN KEY (workitemId) REFERENCES WorkItem(Id);
ALTER TABLE AssociatedWorkItemChangelogs ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE AssociatedWorkItemFiles ADD FOREIGN KEY (WorkItemId) REFERENCES WorkItem(Id);
ALTER TABLE AssociatedWorkItemFiles ADD FOREIGN KEY (FileId) REFERENCES SystemFiles(Id);
ALTER TABLE AssociatedWorkItemMessages ADD FOREIGN KEY (WorkItemId) REFERENCES WorkItem(Id);
ALTER TABLE AssociatedWorkItemMessages ADD FOREIGN KEY (MessageId) REFERENCES WorkItemMessage(Id);
ALTER TABLE AssociatedWrorkItemChildren ADD FOREIGN KEY (WorkItemId) REFERENCES WorkItem(Id);
ALTER TABLE AssociatedWrorkItemChildren ADD FOREIGN KEY (WorkItemChildId) REFERENCES WorkItem(Id);
ALTER TABLE AssociatedWrorkItemChildren ADD FOREIGN KEY (RelationType) REFERENCES WorkItemTypes(Id);
ALTER TABLE Branches ADD FOREIGN KEY (ProjectId) REFERENCES Projects(Id);
ALTER TABLE Notifications ADD FOREIGN KEY (NotificationType) REFERENCES NotificationTypes(Id);
ALTER TABLE Projects ADD FOREIGN KEY (RepositoryId) REFERENCES Repository(Id);
ALTER TABLE SystemFiles ADD FOREIGN KEY (FileType) REFERENCES FileTypes(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (WorkItemTypeId) REFERENCES WorkItemTypes(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (AssignedAccount) REFERENCES UserAccounts(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (StateId) REFERENCES WorkItemStates(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (AreaId) REFERENCES WorkItemAreas(ID);
ALTER TABLE WorkItem ADD FOREIGN KEY (PriorityId) REFERENCES WorkItemPriorities(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (Iteration) REFERENCES WorkItemIterations(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (Severity) REFERENCES WorkItemSeverities(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (Activity) REFERENCES WorkItemActivity(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (ReasonId) REFERENCES WorkItemReasons(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (RelationId) REFERENCES WorkItemRelations(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (RiskId) REFERENCES Risks(Id);
ALTER TABLE WorkItem ADD FOREIGN KEY (ValueAreaId) REFERENCES ValueAreas(Id);
ALTER TABLE WorkItemMessage ADD FOREIGN KEY (SenderId) REFERENCES UserAccounts(Id);