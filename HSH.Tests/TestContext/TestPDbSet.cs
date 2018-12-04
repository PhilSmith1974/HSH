using HSH.Entities;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSH.Tests.TestContext
{
    class TestPropertyDbSet : TestDbSet<Property>
    {
        public override Property Find(params object[] keyValues)
        {
            return this.SingleOrDefault(p => p.Id == (int)keyValues.Single());
        }

        public override Task<Property> FindAsync(params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }

        public override Task<Property> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }
    }

    class TestPropertyTypeDbSet : TestDbSet<PropertyType>
        {
            public override PropertyType Find(params object[] keyValues)
            {
                return this.SingleOrDefault(v => v.Id == (int)keyValues.Single());
            }

            public override Task<PropertyType> FindAsync(params object[] keyValues)
            {
                return Task.FromResult(Find(keyValues));
            }

            public override Task<PropertyType> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
            {
                return Task.FromResult(Find(keyValues));
            }
        }

    class TestPropertyLinkTextDbSet : TestDbSet<PropertyLinkText>
        {
            public override PropertyLinkText Find(params object[] keyValues)
            {
                return this.SingleOrDefault(v => v.Id == (int)keyValues.Single());
            }

            public override Task<PropertyLinkText> FindAsync(params object[] keyValues)
            {
                return Task.FromResult(Find(keyValues));
            }

            public override Task<PropertyLinkText> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
            {
                return Task.FromResult(Find(keyValues));
            }
        }
    }

