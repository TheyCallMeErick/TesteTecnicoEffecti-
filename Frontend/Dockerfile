FROM node:22-alpine AS build
WORKDIR /app

RUN corepack enable

COPY package.json package-lock.json ./
RUN npm install

COPY . ./

ARG VITE_API_URL
ARG VITE_API_PORT

ENV VITE_API_URL=$VITE_API_URL
ENV VITE_API_PORT=$VITE_API_PORT

RUN npm run build

FROM node:22-alpine AS production
WORKDIR /app

RUN npm install -g serve

COPY --from=build /app/dist /app/dist

EXPOSE 80

CMD ["serve", "-s", "dist", "-l", "80"]
