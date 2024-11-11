﻿using System;
using System.Linq;
using System.Text;
using fNbt;


namespace KarolK72.Tools.McNbtUtils
{
    public static class GiveCommandGenerator
    {
        public static string GenerateCommand(NbtCompound rootTag, string recipientSelector)
        {
            // check if id tag is present
            var idTag = rootTag.FirstOrDefault(t => t is NbtString && t is { Name: "id", HasValue: true });
            if (idTag is null)
                throw new ArgumentNullException(nameof(rootTag), "Root tag does not contain a nested 'id' tag needed to generate a Give Command");
            var dataTag = rootTag.FirstOrDefault(t => t is NbtCompound && t is {Name: "tag"});
            if(dataTag is null)
                throw new ArgumentNullException(nameof(rootTag), "Root tag does not contain a nested tag called 'tag' that contains the data necessary for the Give Command");
            return
                $"/give {recipientSelector} {idTag.StringValue} {CommandNbtDataStringGenerator.Generate(dataTag)}";
        }
    }
}