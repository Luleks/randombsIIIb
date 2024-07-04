import { MigrationInterface, QueryRunner } from "typeorm";

export class V51719694720527 implements MigrationInterface {
    name = 'V51719694720527'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "author" DROP COLUMN "firstName"`);
        await queryRunner.query(`ALTER TABLE "author" DROP COLUMN "lastName"`);
        await queryRunner.query(`ALTER TABLE "author" ADD "name" character varying NOT NULL`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "author" DROP COLUMN "name"`);
        await queryRunner.query(`ALTER TABLE "author" ADD "lastName" character varying NOT NULL`);
        await queryRunner.query(`ALTER TABLE "author" ADD "firstName" character varying NOT NULL`);
    }

}
