Training RL Agents with Python and Unity 3d mlagent: https://github.com/Unity-Technologies/ml-agents

Comment 15/10/2017, 11:00 pm:

Currently the map/environment consistis of:
1. obstacle walls that have collision bodies attached to them.
2. A parking lot, with one of the parking spot being the final destination
3. A car. It has a camera that moves with the car and observe the front, and send that oberservation to
the python PPO model and get an action based on it. The car also has a collision body attached,  
when the body hits the obstacles the reward is -1 and the environment reset, and when the body hits
the goal parking spot, the reward is 1. Every action/step that can take the car closer to the goal 
gets a reward of 0.1, otherwise the reward is 0.

Problems:
1. Apparently at the very least this rewarding system is far from good: based on the way the obstacles are
placed, an action that makes the car further away from the goal could eventually take the car closer. Therefore
the car should probably keep a memory of the actions, rewards, and update an entire memory block when it hits
an obstacle or the goal.
2. Currently because of the speed of the simulation, making the size of observation something reasonble, like 
720 * 360, would simply just crush/freeze the app. And if the size is set to something like 32 * 32, it's 
basically useless.
3. Also because of the speed, the number of filters the PPO models uses for the CNNs cannot be too big either, 
the conv1 in the model has 32 filters, conv2 has 64.
4. I tried to use matplotlib to visualize the observation and CNN filters dynamically: the figure would refresh
itself instead of rendering a new one, but so far that's not working. I have to scroll down the list of rendered
activations if I want to see the latest one.
5. The actions the car can take are just move forward/backward, rotate left/right with fixed values, which is 
not realistic and too simple. And realistically the actions should be happening parallellly instead of one by one.
