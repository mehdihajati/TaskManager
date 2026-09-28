using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Tests.Entities
{
    public class ProjectMemberTests
    {
        [Fact]
        public void ChangeRole_WhenMemberIsOwner_Throws()
        {
            var owner = ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid(), ProjectRole.Owner);
            Assert.Throws<InvalidOperationException>(
                () => owner.ChangeRole(ProjectRole.Member, ProjectRole.Owner)
            );
        }
        [Fact]
        public void ChangeRole_WhenMemberIsManager_Throws()
        {
            var owner = ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid(), ProjectRole.Manager);
            Assert.Throws<InvalidOperationException>(
                () => owner.ChangeRole(ProjectRole.Member, ProjectRole.Manager)
            );
        }
        [Fact]
        public void ChangeRole_WhenMemberIsNotManagerOrOwner_Throws()
        {
            var member = ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid(), ProjectRole.Member);
            Assert.Throws<InvalidOperationException>(
                () => member.ChangeRole(ProjectRole.Member, ProjectRole.Member)
                );
        }
        [Fact]
        public void ChangeRole_WhenMemberAlreadyHasThatRole_Throws()
        {
            var owner = ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid(), ProjectRole.Member);
            var ex = Assert.Throws<InvalidOperationException>(
                () => owner.ChangeRole(ProjectRole.Member, ProjectRole.Owner)
            );
            Assert.Contains("already has", ex.Message);
        }
        [Fact]
        public void ChangeRole_WhenRequesterIsOwner_UpdatesRoleAndTimestamp()
        {
            var member = ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid(), ProjectRole.Member);
            var before = DateTimeOffset.UtcNow;
            member.ChangeRole(ProjectRole.Viewer, ProjectRole.Manager);
            Assert.Equal(ProjectRole.Viewer, member.Role);
            Assert.True(member.UpdatedAt >= before);
        }
    }
}