using java.util;
using PicNetML;
using TextMining.DiscoveryLogic.Interfaces;
using TextMining.Entities;
using weka.classifiers;
using weka.classifiers.trees;
using weka.core;
using weka.core.converters;

namespace TextMining.DiscoveryLogic
{
    public class WekaTopicPredictor : ITopicPredictor
    {
        public void Train(DatasetRepresentation datasetRepresentation)
        {
            var cv = Runtime.LoadFromFile<object>("J48", "dataset.arff");

            ConverterUtils.DataSource source = new ConverterUtils.DataSource("dataset.arfffff");
            Instances data = source.getDataSet();

            J48 tree = new J48();
            Evaluation eval = new Evaluation(data);
            eval.crossValidateModel(tree, data, 10, new Random(1));

            throw new System.NotImplementedException();
        }

        public string PredictTopic(DocumentData documentData)
        {
            throw new System.NotImplementedException();
        }
    }
}