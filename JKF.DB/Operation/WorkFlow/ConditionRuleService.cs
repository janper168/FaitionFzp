﻿using JKF.DB.DbContexts;
using JKF.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class ConditionRuleService : BaseService<ConditionRule>
    {
        public ConditionRuleService(BaseDbContext baseDbContext) : base(baseDbContext) { }
    }
}
