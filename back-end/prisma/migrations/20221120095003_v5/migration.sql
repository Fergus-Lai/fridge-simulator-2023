/*
  Warnings:

  - Added the required column `shelf` to the `Freezer` table without a default value. This is not possible if the table is not empty.
  - Added the required column `shelf` to the `Fridge` table without a default value. This is not possible if the table is not empty.

*/
-- AlterTable
ALTER TABLE "Freezer" ADD COLUMN     "shelf" TEXT NOT NULL;

-- AlterTable
ALTER TABLE "Fridge" ADD COLUMN     "shelf" TEXT NOT NULL;
