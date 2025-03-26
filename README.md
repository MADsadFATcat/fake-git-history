# This repository is for fun

Hello! Welcome to this completely useless, yet fun repository. It was created purely for enjoyment and to demonstrate what you can do with Git and GitHub Actions, even if it has no practical value.

## What does this repository do?

This repository generates git commits that are automatically added to my commit map on my GitHub profile.

## Why C#?

C# is not the most obvious choice for this kind of task. But I wanted to show that even for casual tasks like generating commits, C# does just fine. 😎

## How does it work?

1. Once per week, GitHub Actions is triggered.
2. The workflow builds and runs a Docker image.
3. Inside the Docker container, C# code executes Git commands to generate multiple commits and push them to this repository.
4. These commits are automatically displayed on my profile.

## What's the point?

This is all done for fun and to demonstrate the capabilities of GitHub Actions. If you want to try it yourself, go ahead — don’t be afraid to experiment!

I hope you enjoy it! 🎉
