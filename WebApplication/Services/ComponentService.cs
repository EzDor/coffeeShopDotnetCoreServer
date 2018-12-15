using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Controllers.Forms.Components;
using WebApplication.Models;
using WebApplication.Models.Statuses;
using WebApplication.Repositories.Interfaces;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IComponentRepository _componentRepository;

        public ComponentService(IComponentRepository componentRepository)
        {
            _componentRepository = componentRepository;
        }

        /*********************************
        * Public Functions
        *********************************/

        public void CreateComponent(ComponentForm componentForm)
        {
            var component = GetComponent(componentForm.type);
            if (component != null)
            {
                throw new ApplicationException("Component with type " + componentForm.type + " is already exist");
            }

            component = new Components();
            component = PrepareComponent(component, componentForm);
            _componentRepository.Add(component);
            _componentRepository.SaveChanges();
        }

        public void UpdateComponent(UpdatedComponentForm updatedComponentForm)
        {
            var component = GetComponent(updatedComponentForm.componentTypeToUpdate);
            if (component == null)
            {
                throw new ApplicationException("Cannot update component, component with type " +
                                               updatedComponentForm.componentTypeToUpdate + " is not found");
            }

            component = PrepareComponent(component, updatedComponentForm.updatedComponentDetails);
            _componentRepository.SaveChanges();
        }

        public void DeleteComponent(string type)
        {
            var component = GetComponent(type);
            if (component == null)
            {
                throw new ApplicationException("Couldn't find component with type " + type);
            }

            component.Status = ComponentStatus.DISCARDED;
            _componentRepository.SaveChanges();
        }

        public List<Components> GetComponents()
        {
            return _componentRepository.GetAll().ToList();
        }

        public List<Components> GetComponentsByType(List<string> componentTypes)
        {
            return _componentRepository.FindAllByTypeInAndStatus(componentTypes, ComponentStatus.ACTIVE).ToList();
        }

        public void DecreaseAmount(List<Components> components)
        {
            foreach (var component in components)
            {
                DecreaseAmount(component);
            }

            _componentRepository.SaveChanges();
        }

        /*********************************
        * Private Functions
        *********************************/

        private Components GetComponent(string type)
        {
            return _componentRepository.FindByType(type);
        }

        private static Components PrepareComponent(Components component, ComponentForm componentForm)
        {
            component.Amount = componentForm.amount;
            component.Type = componentForm.type.ToLower();
            component.Status = componentForm.status;
            component.Image = componentForm.image;
            component.Name = componentForm.name;
            component.Price = componentForm.price;

            return component;
        }

        private void DecreaseAmount(Components component)
        {
            var newAmount = component.Amount - 1;
            if (newAmount == null || newAmount < 0)
            {
                throw new ApplicationException("Component " + component.Type + " is out of stock");
            }
            else if (newAmount == 0)
            {
                component.Status = ComponentStatus.OUT_OF_STOCK;
            }

            component.Amount = newAmount;
        }
    }
}