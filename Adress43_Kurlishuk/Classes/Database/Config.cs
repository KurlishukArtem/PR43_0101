﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Adress43_Kurlishuk.Classes.Database
{
    public class Config
    {
        public static readonly string connection = "server=student.permaviat.ru;" +
            "uid=ISP_21_2_8;" +
            "pwd=699caSvTn#;" +
            "database=base1_ISP_21_2_8; ";
        public static readonly MySqlServerVersion version = new MySqlServerVersion(new Version(8,0,11));
    }
}  
