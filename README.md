🌐 Live Site

Static Website:
https://azureresumestorage25.z19.web.core.windows.net

Backend API:
https://azureresume-func-25.azurewebsites.net/api/GetVisitorCounter

🏗 Architecture Overview
Browser (HTTPS)
   ↓
Azure Storage Static Website
   ↓ (fetch)
Azure Function App (HTTP Trigger, .NET Isolated)
   ↓
Azure Cosmos DB (SQL API)

Components
Layer	Service	Purpose
Frontend	Azure Storage Static Website	Hosts HTML/CSS/JS resume
Backend	Azure Functions (.NET Isolated, v4)	Visitor counter API
Database	Azure Cosmos DB (SQL API)	Stores visit count
Security	Azure App Settings + CORS	Secure secret handling
Tooling	Azure CLI, Functions Core Tools	Deployment & management
🧠 Design Decisions
Why Azure Functions (Consumption)?

Serverless, event-driven

Scales automatically

Near-zero idle cost

Ideal for low-traffic APIs

Why Cosmos DB?

Native Azure Functions bindings

Simple key-value document model

Low-latency global access

Serverless-friendly

Why Connection String (not Managed Identity)?

Azure Functions Cosmos bindings are optimized for connection strings

Lower complexity for this workload

Secrets stored securely in App Settings

Managed Identity planned as an enterprise upgrade

🔐 Security Considerations

No secrets committed to GitHub

Cosmos DB connection string stored in Function App Application Settings

CORS restricted to the static website origin

HTTPS enforced end-to-end

Mixed-content issues resolved (HTTP → HTTPS)

⚙ Local Development
Prerequisites

Azure CLI

Azure Functions Core Tools

.NET 8 SDK

Node.js (for tooling only)

Python (for local static site)

Run Backend
cd api
func start --cors "*"


API available at:

http://localhost:7071/api/GetVisitorCounter

Run Frontend
cd frontend
python -m http.server 5500 --bind localhost


Open:

http://localhost:5500

🚀 Deployment (Production)
Backend
func azure functionapp publish azureresume-func-25

Frontend
az storage blob upload-batch `
  --account-name azureresumestorage25 `
  --account-key <key> `
  -s frontend `
  -d '$web' `
  --overwrite

📈 Performance Notes

Cold start on Consumption plan: ~2–3 seconds

Warm response: ~400–500 ms

Frontend shows loading placeholder until API returns

This tradeoff was accepted to minimize cost.

🧪 Debugging & Issues Solved

This project intentionally exposed real engineering problems:

Azure Functions cold starts

CORS configuration (local vs production)

HTTPS mixed-content blocking

Script load order and DOM timing

Azure CLI + PATH issues

Binding configuration mismatches

Environment switching (localhost vs production)

💰 Cost Profile (Monthly Estimate)
Service	Plan	Est. Cost
Storage Static Website	Standard LRS	~$0.01
Azure Functions	Consumption	~$0.00–$1
Cosmos DB	Free Tier	$0
Total		~$1/month
🛣 Planned Enhancements (Cost-Conscious)

The following upgrades improve quality without breaking the bank.

✅ 1. Application Insights (FREE tier)

Why: Observability, request timing, error tracking
Cost: $0 (basic usage)

Action:
Enable Application Insights on the Function App.

✅ 2. Scheduled Warm-Up (Almost Free)

Why: Reduce cold starts
How: Timer-triggered function every 5 minutes
Cost: Negligible

✅ 3. GitHub Actions CI/CD (FREE)

Why: Automated deployment
Cost: $0 for public repos

Push → auto deploy Function

Push → auto upload frontend

⚠️ Optional (Higher Cost, Not Required)
Upgrade	Why	Cost
Functions Premium	No cold starts	~$20+/mo
Managed Identity + Cosmos RBAC	Enterprise security	Complexity
Azure Front Door	Global performance	$$$

These are intentionally deferred to keep costs low.
