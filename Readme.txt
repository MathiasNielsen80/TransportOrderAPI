Exercise:
I have chosen to implement an API to create and retrieve transport orders. 
The implementation is based on the CQRS pattern, where commands are used to change the state of the transport order, and queries would be used to retrieve the state of the transport order. For simplicity a simple 
read service implements the queries. The domain is the core of the application, as described in clean/onion architecture.

With respect to the timeframe given, I have chosen to model the concept of a transport order with just three states, simple protection against invalid state changes, and events to notify about state changes.
Based on the Outbox pattern the events would be stored atomically with the transport order aggregate using a transaction, and read by a separate event handler that sends the events to a message broker or queue, 
e.g. Kafka or NServicebus ensuring consistency.

Many things are left out of scope. For example, the API is not secured, there is no validation of input, and there is no error handling.

I find the following engineering practices to be essential:
- Use a version control system to manage codebase, e.g. GIT.
- Use Pull requests with mandatory code reviews to ensure code quality and knowledge sharing, and enforce mandatory links to Work Items.
- Use a CI/CD pipeline to automate the build, test, and deployment process.
- Use a containerization tool like Docker to package our application and its dependencies.
- Use an orchestration tool like Kubernetes to manage our containers in a production environment.
- Use a monitoring tool to monitor the health of our applications, e.g. Prometheus.
- Use a logging tool to collect and analyze logs from our application, e.g. the ELK stack. Also enable distributed tracing, e.g. Jaeger.
- Use a security scanning tool to identify and fix vulnerabilities in our codebase, e.g. Snyk.
- Use a collaboration tool like Slack to communicate with our team and stakeholders.
- Use a static code analysis tool to identify and fix code quality issues, hotspots, and technical debt, e.g. CodeScene

As a Senior Engineering Manager, my focus will be on the following:
-Ensure that the team knows why they are building the new platform and what value it will bring to the LEGO group. 
-Ensure that slices of the platform are delivered incrementally to get feedback from stakeholders and users, and to achive value early.
-Ensure that the team has the right skills and knowledge to build and operate the new platform.
-Ensure that the team has the right tools and processes in place to build and operate the new platform.
-Ensure that the team has the right culture and mindset to build and operate the new platform.
-Participate actively to value creation.