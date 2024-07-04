import { MigrationInterface, QueryRunner } from "typeorm";

export class V61719736572055 implements MigrationInterface {
    name = 'V61719736572055'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "book" DROP COLUMN "pdfFile"`);
        await queryRunner.query(`ALTER TABLE "book" ADD "pdfFile" character varying NOT NULL`);
        await queryRunner.query(`ALTER TABLE "book" DROP COLUMN "cover"`);
        await queryRunner.query(`ALTER TABLE "book" ADD "cover" character varying NOT NULL`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "book" DROP COLUMN "cover"`);
        await queryRunner.query(`ALTER TABLE "book" ADD "cover" bytea NOT NULL`);
        await queryRunner.query(`ALTER TABLE "book" DROP COLUMN "pdfFile"`);
        await queryRunner.query(`ALTER TABLE "book" ADD "pdfFile" bytea NOT NULL`);
    }

}
