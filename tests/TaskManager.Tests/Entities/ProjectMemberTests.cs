using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Tests.Entities
{
    public class ProjectMemberTests
    {
        //Create methods tests
        [Fact]
        public void Create_whenProjectIdIsEmpty_Throws()
        {
            Assert.Throws<ArgumentException>(() => ProjectMember.Create(Guid.Empty, Guid.NewGuid()));
        }
        [Fact]
        public void Create_whenUserIdIsEmpty_Throws()
        {
            Assert.Throws<ArgumentException>(() => ProjectMember.Create(Guid.NewGuid(), Guid.Empty));
        }
        [Fact]
        public void Create_whenProjectRoleIsNotInEnum_Throws()
        {
            Assert.Throws<ArgumentException>(() => ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid(), (ProjectRole)999));
        }
        [Fact]
        public void Create_WithoutRole_DefaultsToMember()
        {
            var member = ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid());

            Assert.Equal(ProjectRole.Member, member.Role);
        }
        //ChangeRole method Tests
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
        //Remove Mehod tests
        [Fact]
        public void RemoveMember_WhenSelectedMemberIsOwner_Throws()
        {
            var member = ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid(), ProjectRole.Owner);
            var ex = Assert.Throws<InvalidOperationException>(
                () => member.Remove(member.Role));
            Assert.Contains("owner role", ex.Message);
        }
        [Theory]
        [InlineData(ProjectRole.Manager)]
        [InlineData(ProjectRole.Member)]
        [InlineData(ProjectRole.Viewer)]
        public void RemoveMember_whenMemberIsNotProjectOwnerOrManager_throws(ProjectRole requesterRole)
        {
            var member = ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid(), ProjectRole.Manager);
            var ex = Assert.Throws<InvalidOperationException>(
                () => member.Remove(requesterRole));
            Assert.Contains("Only owner", ex.Message);
        }
        [Fact]
        public void RemoveMember_whenViewerOrMemberTryToRemoveOtherMembers()
        {
            var member = ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid(), ProjectRole.Member);
            var ex = Assert.Throws<InvalidOperationException>(
                () => member.Remove(member.Role)
                );
            Assert.Contains("You dont", ex.Message);
        }
        [Fact]
        public void RemoveMember_whenEverythingIsFine()
        {
            var member = ProjectMember.Create(Guid.NewGuid(), Guid.NewGuid(), ProjectRole.Member);
            var before = DateTimeOffset.UtcNow;
            member.Remove(ProjectRole.Manager);
            Assert.True(member.IsDeleted);
            Assert.NotNull(member.DeletedAt);
            Assert.True(member.DeletedAt >= before);
        }
    }
}