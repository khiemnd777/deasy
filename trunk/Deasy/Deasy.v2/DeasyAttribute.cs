using System;
using Deasy.Infrastructure.DependencyManagement;

namespace Deasy
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited=true, AllowMultiple=true)]
    public class DeasyAttribute : Attribute
    {
        public LifeStyle LifeStyle { private set; get; }
        public string Key { private set; get; }
       
        public DeasyAttribute(string key = "", LifeStyle lifeStyle = LifeStyle.LifetimeScope)
        {
            Key = key;
            LifeStyle = lifeStyle;
        }
    }
}