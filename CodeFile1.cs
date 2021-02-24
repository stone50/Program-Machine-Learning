using System;

public class CalcNode
{
    public float current_value;     //current value of node
    public float[] weights;         //weights for outbound values
    public float[] inbound_values;  //inbound values used to calculate current value
    public int inbound_index;       //index of the inbound value to override
    public float bias;              //node bias is added to the current value

    public CalcNode()
    {
        current_value = 0;
        weights = new float[0];
        inbound_values = new float[0];
        inbound_index = 0;
        bias = 0;
    }

    public CalcNode(int in_count, int mid_count, int out_count)
    {
        current_value = 0;
        weights = new float[mid_count + out_count - 1];
        inbound_values = new float[in_count + mid_count + out_count - 1];
        inbound_index = 0;
        bias = 0;
    }

    //calculates the current value of the node
    public void calcCurrentValue()
    {
        float sum = bias;
        foreach (float value in inbound_values)
        {
            sum += value;
        }
        current_value = sigmoid(sum);
    }

    //returns a float between -1 and 1 based on num
    float sigmoid(float num)
    {
        return (float)Math.Tanh(num);
    }
};

public class InputNode
{
    public float[] weights; //weights for outbound values

    public InputNode()
    {
        weights = new float[0];
    }

    public InputNode(int mid_count, int out_count)
    {
        weights = new float[mid_count + out_count];
    }
};

public class Network
{
    public InputNode[] input_nodes;
    public CalcNode[] middle_nodes;
    public CalcNode[] output_nodes;

    private readonly Random rand_gen = new Random();

    public Network()
    {
        input_nodes = new InputNode[0];
        middle_nodes = new CalcNode[0];
        output_nodes = new CalcNode[0];
    }

    public Network(int in_count, int mid_count, int out_count)
    {
        input_nodes = new InputNode[in_count];
        middle_nodes = new CalcNode[mid_count];
        output_nodes = new CalcNode[out_count];

        for (int i = 0; i < in_count; i++)
        {
            input_nodes[i] = new InputNode();
        }
        for (int i = 0; i < mid_count; i++)
        {
            middle_nodes[i] = new CalcNode();
        }
        for (int i = 0; i < out_count; i++)
        {
            output_nodes[i] = new CalcNode();
        }
    }

    void step(float[] inputs)
    {

        //calculate current value using inbound values and prepare to override inbound values
        foreach (CalcNode mid_node in middle_nodes)
        {
            mid_node.calcCurrentValue();
            mid_node.inbound_index = 0;
        }
        foreach (CalcNode out_node in output_nodes)
        {
            out_node.calcCurrentValue();
            out_node.inbound_index = 0;
        }

        //send outbound values for next step

        //send values from inputs
        for (int i = 0; i < input_nodes.Length; i++)
        {
            float current_input = sigmoid(inputs[i]);
            int weight_index = 0;
            foreach (CalcNode mid_node in middle_nodes)
            {
                mid_node.inbound_values[mid_node.inbound_index++] = current_input * input_nodes[i].weights[weight_index++];
            }
            foreach (CalcNode out_node in output_nodes)
            {
                out_node.inbound_values[out_node.inbound_index++] = current_input * input_nodes[i].weights[weight_index++];
            }
        }

        //send values from middle nodes
        foreach (CalcNode current_node in middle_nodes)
        {
            int weight_index = 0;
            foreach (CalcNode mid_node in middle_nodes)
            {
                if (current_node != mid_node)
                {
                    mid_node.inbound_values[mid_node.inbound_index++] = current_node.current_value * current_node.weights[weight_index++];
                }
            }
            foreach (CalcNode out_node in output_nodes)
            {
                out_node.inbound_values[out_node.inbound_index++] = current_node.current_value * current_node.weights[weight_index++];
            }
        }

        //send values from output nodes
        foreach (CalcNode current_node in output_nodes)
        {
            int weight_index = 0;
            foreach (CalcNode mid_node in middle_nodes)
            {
                mid_node.inbound_values[mid_node.inbound_index++] = current_node.current_value * current_node.weights[weight_index++];
            }
            foreach (CalcNode out_node in output_nodes)
            {
                if (current_node != out_node)
                {
                    out_node.inbound_values[out_node.inbound_index++] = current_node.current_value * current_node.weights[weight_index++];
                }
            }
        }
    }

    //returns an array of the current values for each of the output nodes
    public float[] getOutputs()
    {
        float[] output_values = new float[output_nodes.Length];
        for (int i = 0; i < output_nodes.Length; i++)
        {
            output_values[i] = output_nodes[i].current_value;
        }
        return output_values;
    }

    //sets each value within the network to a random float between -1 and 1
    public void randomize()
    {

        //randomizes each property of the input nodes
        foreach (InputNode in_node in input_nodes)
        {
            for (int i = 0; i < in_node.weights.Length; i++)
            {
                in_node.weights[i] = random(-1, 1);
            }
        }

        //randomizes each property of the middle nodes
        foreach (CalcNode mid_node in middle_nodes)
        {
            mid_node.current_value = random(-1, 1);
            for (int i = 0; i < mid_node.weights.Length; i++)
            {
                mid_node.weights[i] = random(-1, 1);
            }
            for (int i = 0; i < mid_node.inbound_values.Length; i++)
            {
                mid_node.inbound_values[i] = random(-1, 1);
            }
            mid_node.bias = random(-1, 1);
        }

        //randomizes each property of the output nodes
        foreach (CalcNode out_node in output_nodes)
        {
            out_node.current_value = random(-1, 1);
            for (int i = 0; i < out_node.weights.Length; i++)
            {
                out_node.weights[i] = random(-1, 1);
            }
            for (int i = 0; i < out_node.inbound_values.Length; i++)
            {
                out_node.inbound_values[i] = random(-1, 1);
            }
            out_node.bias = random(-1, 1);
        }
    }

    //generates a random float between min and max
    float random(float min, float max)
    {
        return (float)((rand_gen.NextDouble() * (max - min)) + min);
    }

    //returns a float between -1 and 1 based on num
    float sigmoid(float num)
    {
        return (float)Math.Tanh(num);
    }
};