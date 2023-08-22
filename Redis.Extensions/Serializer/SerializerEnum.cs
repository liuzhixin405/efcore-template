﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Extensions.Serializer
{
    /// <summary>
    /// 序列化类型
    /// </summary>
  public  enum SerializerEnum
    {
        /// <summary>
        /// json序列化
        /// </summary>
        JSON,
        /// <summary>
        /// bson序列化
        /// </summary>
        BSON,
        /// <summary>
        /// protobuf序列化
        /// </summary>
        PROTOBUF
    }
}
