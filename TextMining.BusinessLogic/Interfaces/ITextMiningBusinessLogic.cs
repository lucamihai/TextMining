﻿using TextMining.Entities;

namespace TextMining.BusinessLogic.Interfaces
{
    public interface ITextMiningBusinessLogic
    {
        DocumentData GetDocumentDataFromXmlFile(string filepath);
    }
}