using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {

        private readonly IList<Subscription> _subscriptions;
        private readonly IList<string> Notification;

        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;

            _subscriptions = new List<Subscription>();

            if (string.IsNullOrEmpty(name.FirstName))
            {
                Notification.Add("Nome Inválido.");
            }
        }
        public Name Name { get; set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }

        //Com o List, a pessoa que pode estar mexendo no código, pode acessar o objeto estudante e sua assinatura 
        //e incluir uma nova assinatura, sem saber da regra de negócio em que 1 aluno pode ter apenas uma assinatura em seu nome
        //public List<Subscription> Subscriptions { get; private set; }

        //Com o ReadOnlyCollection ele não deixa adicionar o metodo add e assim informar uma nova assinatura
        public IReadOnlyCollection<Subscription> Subscriptions
        {
            get
            {
                return _subscriptions.ToArray();
            }
        }

        public void AddSubscription(Subscription subscription)
        {
            //Se já tiver uma assinatura ativa, cancela.

            //Cancela todas as assinaturas e setar está como a principal.
            foreach (var sub in Subscriptions)
            {
                subscription.Inactivate();
            }

            _subscriptions.Add(subscription);
        }

    }
}
