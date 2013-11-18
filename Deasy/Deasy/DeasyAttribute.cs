using System;
using Deasy.Infrastructure.DependencyManagement;

namespace Deasy
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited=true, AllowMultiple=true)]
    public class DeasyAttribute : Attribute
    {
        public ComponentLifeStyle LifeStyle { private set; get; }
        public string Key { private set; get; }
       
        public DeasyAttribute(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            Key = key;
            LifeStyle = lifeStyle;
        }
    }
}