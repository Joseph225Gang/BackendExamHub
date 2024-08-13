namespace BackendExamHub.Entity
{
    public class Myoffice_ExcuteionLog
    {
        public int DeLog_AuthID { get; set; }
        public string DeLog_StoredPrograms { get; set; }
        public Guid DeLog_GroupID { get; set; }
        public bool DeLog_isCustomDebug { get; set; }
        public string eLog_ExecutionProgram { get; set; }
        public string DeLog_ExecuteionInfo { get; set; }
        public bool DeLog_VerifyNeeded { get; set; }
        public DateTime exelog_nowdatetime { get; set; }
    }
}
