# System for Root Motion Movement in Unity

The classic problem of *"how do you sync the walk cycle speed with the actual translation speed of the character so they don't moonwalk"* is a common issue in Unity game development. The solutions to this kind of problem is not very well documented.
Often it requires a lot of manual tweaking and messy code, and usually still ends up with the characters looking like they're "moonwalking", i.e sliding on the floor.

## Without Root Motion Animations
https://user-images.githubusercontent.com/16218331/206888383-0f849e11-8ddd-4662-887b-42d787cd576c.mp4



## With Root Motion
https://user-images.githubusercontent.com/16218331/206888333-a2276afe-11c0-4b2a-b0de-f5404046c94f.mp4


Making use of blend tree's and controlling the character root motion through a script, I've designed a system here that allows you to minimize "moonwalking" in any movement scenario. The particular scenario I've used is for Nav Mesh Agents, since that's when moonwalking tends to be the worst.
