/*
  Warnings:

  - Added the required column `quantity` to the `Freezer` table without a default value. This is not possible if the table is not empty.

*/
-- AlterTable
ALTER TABLE "Freezer" ADD COLUMN     "quantity" INTEGER NOT NULL,
ALTER COLUMN "expDate" SET DATA TYPE TEXT;
