/*
  Warnings:

  - Added the required column `quantity` to the `Fridge` table without a default value. This is not possible if the table is not empty.

*/
-- AlterTable
ALTER TABLE "Fridge" ADD COLUMN     "quantity" INTEGER NOT NULL;
