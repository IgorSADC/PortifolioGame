using System;

[AttributeUsage(AttributeTargets.Field)]
public class ObservableOnPipelineAttribute : Attribute
{
    public string customNameTrainslation;
    public ObservableOnPipelineAttribute(string nameTranslation)
    {
        this.customNameTrainslation = nameTranslation;
    }
    public ObservableOnPipelineAttribute(){
        this.customNameTrainslation = null;
    }
}
