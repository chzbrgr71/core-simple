# Dockerfile for front-end. 
# 
#

# Pull base image.
FROM readytalk/nodejs

# Copy files
COPY ../. /frontend

# Define working directory.
WORKDIR /frontend/FrontendService.Code

RUN chmod +x run.sh

EXPOSE 8507

# Define default command.
ENTRYPOINT ["./run.sh"]