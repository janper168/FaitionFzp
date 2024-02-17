using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Models.WorkFlow
{
    //任务节点当前的状态值
    public class TaskNodeStatus
    {
        public const int UnReached = 1;//未到达
        public const int Waiting = 2;//待处理（审批，知会，子流程）
        public const int Draft = 3;//草稿
        public const int Submit = 4;//已提交
        public const int UnEnd = 5;//未结束
        public const int End = 6;//已结束
        public const int ProcessPass = 7;//处理通过
        public const int ProcessBack = 8;//处理返回
        public const int ConditionYes = 9;//判断是
        public const int ConditionNo = 10;//判断否
    }
}
