using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.Queues;

public class Queue1(SimCore core) : FifoQueue<Order>(core);

public class Queue2(SimCore core) : FifoQueue<Order>(core);

public class Queue3(SimCore core) : FifoQueue<Order>(core);

public class Queue4(SimCore core) : FifoQueue<Order>(core);