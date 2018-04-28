ALTER TABLE dbo.Request
 ADD CONSTRAINT Nurse_Request
  FOREIGN KEY (NurseId) REFERENCES dbo.AspNetUsers(Id)
   ON UPDATE No Action
   ON DELETE Set Null