# Developer Onboarding Guide
Author: kb02  
Date: 2026-03-10

Welcome! This guide helps new developers get productive quickly with this repository. It covers local setup, project layout, the normal development workflow, common gotchas, debugging tips, and the important files to know.

If anything below is unclear or doesn't match the repository contents, open an issue or ask the team (see README for contacts).

---

## Quick start — high level
1. Install prerequisites (see next section).
2. Clone the repository and copy environment templates.
3. Start services locally (database, cache, optional external mocks).
4. Run the app and the test suite.
5. Make a small change -> run tests -> open a PR.

---

## Prerequisites
Install the tools and accounts you will need:
- Git (>= 2.30)
- Docker & Docker Compose (for local infra and reproducible dev environment)
- Language runtime(s) used by the repo (Node.js / Python / Go / etc.). Check `.nvmrc`, `pyproject.toml`, or repo README for exact versions.
- Package manager: npm / yarn / pip / Poetry as appropriate.
- Database client (psql / mysql client) for debugging/local queries.
- A code editor with recommended extensions (e.g., VS Code + EditorConfig + language extensions).

Note: If the repo provides a developer container or devbox (e.g., .devcontainer), use it to avoid local toolchain differences.

---

## Setup — step-by-step
Below are canonical steps. Replace placeholders with repo-specific commands where needed.

1. Clone
```bash
git clone git@github.com:your-org/your-repo.git
cd your-repo
```

2. Check out the right runtime version
```bash
# Node example
nvm use
# Python example
pyenv local $(cat .python-version)
```

3. Copy environment config
```bash
cp .env.example .env
# Edit .env to add secrets and credentials (see Important files below)
```

4. Start supporting services
- Option A: Docker Compose
```bash
docker compose up -d
```
- Option B: Start local DB/Redis services manually if not using Docker

5. Install dependencies
```bash
# JS/TS example
npm ci
# or
yarn install

# Python example
pip install -r requirements.txt
```

6. Run database migrations and seed data
```bash
# Replace with the project's migration command:
npm run migrate
npm run seed
```

7. Start dev servers
```bash
# API / backend
npm run dev:api

# Worker (in another terminal)
npm run dev:worker

# Front-end (if present)
npm run dev:web
```

8. Run test suite
```bash
npm test
# or for full coverage
npm run test:watch
```

---

## Project structure (common layout)
This section explains what you will likely see at the repo root and what each area is responsible for. Adjust to match this repo's actual layout when you review files.

- README.md — Getting-started / high-level audience docs.
- docs/ — Architecture, runbooks, onboarding, and other docs (you are in it).
- package.json / pyproject.toml / go.mod — project metadata and scripts.
- src/ or app/ — application code (API controllers, services, models).
  - src/api/ — HTTP handlers, routing and request validation
  - src/services/ — core business logic
  - src/models/ — ORM models or domain entities
  - src/jobs/ or src/workers/ — background job handlers
  - src/config/ — environment and runtime configuration
- migrations/ — DB migrations
- scripts/ — helper scripts (local seeding, maintenance)
- tests/ — unit, integration, and E2E tests
- infra/ or deployment/ — IaC, k8s manifests, Terraform
- docker-compose.yml / Dockerfile — local dev and production container definitions
- .github/workflows/ — CI definitions

If the repo deviates from this common structure, update this doc (docs/onboarding-guide.md) with the correct mapping.

---

## Development workflow
This is the typical flow used by contributors.

1. Create a feature branch off main
```bash
git checkout -b feat/short-descriptive-name
```
2. Implement small, testable changes. Keep commits atomic and focused.
3. Run unit tests / linters locally:
```bash
npm run lint
npm test
```
4. Push branch and open a Pull Request (PR) targeting the default branch (often `main` or `develop`).
5. Add a clear PR description: what changed, why, how to test, and related issue(s).
6. Request reviews from the team and address review feedback via additional commits.
7. Merge after approvals and green CI.

Branch naming and PR rules:
- Branch: feat/, fix/, chore/, docs/ prefixes
- Rebase or use merge commits depending on repo policy (check CONTRIBUTING.md)
- Include test coverage for logic changes

Release/deploy notes:
- CI runs tests, linters, and build steps. The workflow files in .github/workflows define pipeline behavior — inspect them when adding new build steps.

---

## Testing strategy
- Unit tests: fast, isolated, run locally and in CI
- Integration tests: may require DB/cache; often run with Docker Compose in CI
- E2E tests: simulate user flows (may run in a dedicated pipeline)
- Mocks/fakes: prefer using them for external APIs in unit tests; keep a small number of integration tests against real or recorded endpoints

Commands:
```bash
npm test              # run tests once
npm run test:watch    # run in watch mode
npm run test:ci       # test with CI-like settings
```

---

## Important files
Know these files — they are often the first place to look for answers.

- README.md — high-level repo goals and usage
- docs/* — architecture and runbooks (including this file)
- .env.example — environment variables required for local dev
- .gitignore — files not committed
- package.json / pyproject.toml — scripts and dependencies
- docker-compose.yml — local infra composition
- Dockerfile — build instructions for containers
- migrations/ — database schema changes
- .github/workflows/* — CI pipeline definitions
- src/config/* — default config and how env vars are consumed
- src/logging/* or logging configuration — where logs are configured and how to increase verbosity

---

## Common pitfalls (and how to avoid them)
1. Missing or incorrect environment variables
   - Symptom: app crashes on startup, fails auth, or cannot connect to DB.
   - Fix: copy .env.example to .env and fill values; check README for secret store or vault usage.

2. Database migrations out of sync
   - Symptom: app throws "relation does not exist" or missing columns.
   - Fix: run the migration command before starting the app; verify migration history table.

3. Different runtime versions locally vs CI
   - Symptom: "unexpected token" or dependency install fails.
   - Fix: use version managers (nvm/pyenv) or the dev container; check .nvmrc / .python-version.

4. Docker cache / stale build artifacts
   - Symptom: changes not reflected in containerized app.
   - Fix: rebuild images with no-cache if needed: `docker compose build --no-cache`.

5. Port conflicts
   - Symptom: service fails to bind to a port.
   - Fix: change ports in .env or stop the conflicting service (e.g., another local DB).

6. Relying on third-party APIs in local dev
   - Symptom: flaky local runs or rate limiting.
   - Fix: use recorded responses, local mocks, or sandbox/test API keys.

7. Forgetting to run background worker
   - Symptom: tasks are queued but nothing processes them.
   - Fix: ensure worker process is started (see Setup step 7).

8. Running tests that need the DB without starting it
   - Symptom: integration tests fail.
   - Fix: start local DB (or use testcontainers) before running tests.

9. Secrets accidentally committed
   - Symptom: credentials visible in repo history.
   - Fix: remove from history, rotate secrets, and add/verify .gitignore and pre-commit checks.

---

## Debugging tips
1. Read logs first
   - Increase verbosity via environment variables (LOG_LEVEL=debug) if available.
   - Check both API logs and worker logs.

2. Reproduce locally
   - Isolate the failing component (API, worker, DB). Create a minimal repro and write a unit test to reproduce.

3. Check health endpoints
   - Many services expose `/health` or `/ready` endpoints to confirm dependencies are reachable.

4. Inspect the database
   - Connect with psql or DB client to verify rows and schema. Use migration history table to confirm applied migrations.

5. Check the job/queue state
   - Look at queue monitoring UI (e.g., Bull Board, Sidekiq Web) or query the queue store (Redis) for stalled jobs and error messages.

6. Use the debugger
   - Start the app with an inspector (Node: `node --inspect`) and attach your editor to set breakpoints.

7. Run tests with extra output
   - Use verbose test flags and run only the failing tests to iterate faster.

8. Rebuild and clean caches
```bash
# JS
rm -rf node_modules dist
npm ci
npm run build

# Docker
docker compose down --volumes --remove-orphans
docker compose up --build
```

9. Use git bisect if a regression was introduced
```bash
git bisect start
git bisect bad       # current commit
git bisect good v1.2 # last known good tag/commit
# follow prompts to find the offending commit
```

10. Ask for help—include:
- Steps to reproduce
- Expected vs actual behavior
- Error logs and stack traces
- Relevant config (.env values redacted)
- Recent commits / PRs that touched related code

---

## Onboarding checklist for a new developer
- [ ] Can run the app locally (API + worker + DB) end-to-end
- [ ] Can run the test suite and fix failing tests locally
- [ ] Has a working dev branch and can open a PR
- [ ] Knows where logs and monitoring dashboards are
- [ ] Knows who to contact for infra or domain questions
- [ ] Read the README and docs/architecture files

---

## Who to contact
- See README.md for team contacts and ownership.
- If you don't know who to ask, open an issue labeled "help-wanted" or ping the onboarding buddy assigned to you.
