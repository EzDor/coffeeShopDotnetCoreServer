
using System.Collections.Generic;
using WebApplication.Controllers.Forms.Components;
using WebApplication.Models;

namespace WebApplication.Services.Interfaces
{
    public interface IComponentService
    {
        void CreateComponent(ComponentForm componentForm);
        void UpdateComponent(UpdatedComponentForm updatedComponentForm);
        void DeleteComponent(string type);
        List<Components> GetComponents();
        List<Components> GetComponentsByType(List<string> componentTypes);
        void DecreaseAmount(List<Components> components);
        
    }
}