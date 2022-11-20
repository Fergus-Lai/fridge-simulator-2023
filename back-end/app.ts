import { PrismaClient } from "@prisma/client";
import express, { Request, Response } from "express";

const app = express();
const port = parseInt(process.env.PORT || "3000");
const prisma = new PrismaClient();

app.use(express.json());

app.get("/user", async (req: Request, res: Response) => {
  const { id } = req.body;
  try {
    let user = await prisma.user.findUniqueOrThrow({
      where: { id },
      include: { fridge: true, freezer: true },
    });
    res.json(user);
  } catch (error) {
    res.status(404);
  }
});

app.post("/user", async (req: Request, res: Response) => {
  const { name } = req.body;
  let user = await prisma.user.create({ data: { name: name } });
  res.json(user);
});

app.put("/user", async (req: Request, res: Response) => {
  const { id, name } = req.body;
  try {
    let user = await prisma.user.update({
      where: { id },
      data: { name },
    });
    res.json(user);
  } catch (error) {
    res.status(404);
  }
});

app.delete("/user", async (req: Request, res: Response) => {
  const { id } = req.body;
  try {
    let user = await prisma.user.delete({
      where: { id },
    });
    res.json(user);
  } catch (error) {
    res.status(404);
  }
});

app.get("/fridges", async (req: Request, res: Response) => {
  const { id } = req.body;
  try {
    let user = await prisma.user.findUniqueOrThrow({
      where: { id },
      select: { fridge: { orderBy: { expDate: "desc" } } },
    });
    res.json(user);
  } catch (error) {
    res.status(404);
  }
});

app.post("/fridge", async (req: Request, res: Response) => {
  let { id, name, quantity, type, expDate } = req.body;
  try {
    let item = await prisma.fridge.create({
      data: {
        name,
        type,
        quantity: parseInt(quantity),
        expDate,
        user: { connect: { id } },
      },
    });
    res.json(item);
  } catch (error) {
    res.status(404);
  }
});

app.put("/fridge", async (req: Request, res: Response) => {
  let { id, name, type, expDate, quantity } = req.body;
  try {
    let item = await prisma.fridge.update({
      where: {
        id,
      },
      data: {
        name,
        type,
        quantity: parseInt(quantity),
        expDate,
      },
    });
    res.json(item);
  } catch (error) {
    res.status(404);
  }
});

app.delete("/fridge", async (req: Request, res: Response) => {
  let { id } = req.body;
  try {
    let item = await prisma.fridge.delete({
      where: {
        id,
      },
    });
    res.json(item);
  } catch (error) {
    res.status(404);
  }
});

app.get("/freezers", async (req: Request, res: Response) => {
  const { id } = req.body;
  try {
    let user = await prisma.user.findUniqueOrThrow({
      where: { id },
      select: { freezer: { orderBy: { expDate: "desc" } } },
    });
    res.json(user);
  } catch (error) {
    res.status(404);
  }
});

app.post("/freezer", async (req: Request, res: Response) => {
  let { id, name, quantity, type, expDate } = req.body;
  try {
    let item = await prisma.freezer.create({
      data: {
        name,
        type,
        quantity: parseInt(quantity),
        expDate,
        user: { connect: { id } },
      },
    });
    res.json(item);
  } catch (error) {
    res.status(404);
  }
});

app.put("/freezer", async (req: Request, res: Response) => {
  let { id, name, type, expDate, quantity } = req.body;
  try {
    let item = await prisma.freezer.update({
      where: {
        id,
      },
      data: {
        name,
        type,
        quantity: parseInt(quantity),
        expDate,
      },
    });
    res.json(item);
  } catch (error) {
    res.status(404);
  }
});

app.delete("/freezer", async (req: Request, res: Response) => {
  let { id } = req.body;
  try {
    let item = await prisma.freezer.delete({
      where: {
        id,
      },
    });
    res.json(item);
  } catch (error) {
    res.status(404);
  }
});

const server = app.listen(port, () => {
  console.log(`Server started on Port ${port}`);
});
