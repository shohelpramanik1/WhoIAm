# WHO I AM - Contributing Guide

## Getting Started

1. Clone the repository
2. Follow [SETUP.md](../SETUP.md) for local development
3. Create a feature branch: `git checkout -b feature/description`
4. Make your changes
5. Write tests
6. Submit a pull request

## Code Style

### C# Code Style

- Follow Microsoft C# coding conventions
- Use meaningful variable names
- Keep methods focused and small
- Add XML documentation for public APIs
- Use LINQ where appropriate

### TypeScript/React Code Style

- Use functional components
- Follow React hooks best practices
- Add TypeScript types for all functions
- Use meaningful component names
- Keep components small and reusable

## Git Workflow

1. Create feature branch from `main`
2. Commit with clear messages
3. Push to your fork
4. Create pull request with description
5. Code review and merge

## Pull Request Guidelines

- Describe what the PR does
- Link related issues
- Include before/after screenshots if UI changes
- Ensure all tests pass
- Add tests for new functionality
- Update documentation if needed

## Testing Requirements

- Unit tests for business logic
- Integration tests for APIs
- Test coverage minimum: 70%
- Run tests locally before pushing

```bash
dotnet test
npm test  # in frontend
```

## Commit Message Format

```
<type>: <subject>

<body>

<footer>
```

Types:
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation
- `style`: Code style changes
- `refactor`: Code refactoring
- `test`: Test additions
- `chore`: Build/tooling changes

Example:

```
feat: add mood tracking feature

Implement mood entry creation and history tracking
- Add MoodEntry entity
- Create MoodService
- Add API endpoints
- Add React components

Closes #123
```

## Development Phases

Work is organized in phases. Each phase should be completed before moving to the next:

1. **Phase 1**: Foundation (Auth, User, Config)
2. **Phase 2**: Virtual Identities
3. **Phase 3**: Social Features
4. **Phase 4**: Communities
5. **Phase 5**: Emotional Features
6. **Phase 6**: Enemy System
7. **Phase 7**: Real-Life Missions
8. **Phase 8**: Messaging
9. **Phase 9**: Safety & Moderation
10. **Phase 10**: Administration
11. **Phase 11**: Monetization
12. **Phase 12**: Production

## Reporting Issues

When reporting bugs:

1. Use descriptive title
2. Describe expected behavior
3. Describe actual behavior
4. Include steps to reproduce
5. Attach screenshots/logs if relevant
6. Specify environment (OS, browser, etc.)

## Code Review Checklist

- [ ] Code follows style guidelines
- [ ] Tests are included
- [ ] Documentation is updated
- [ ] No debugging code
- [ ] Error handling is proper
- [ ] Performance is acceptable
- [ ] Security concerns addressed
- [ ] Database changes have migrations

## Resources

- [Architecture Documentation](ARCHITECTURE.md)
- [Deployment Guide](DEPLOYMENT.md)
- [Setup Guide](../SETUP.md)
- [Microsoft C# Conventions](https://docs.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [React Best Practices](https://react.dev/learn)
