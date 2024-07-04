import { MigrationInterface, QueryRunner } from "typeorm";

export class V21719561539562 implements MigrationInterface {
    name = 'V21719561539562'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "book" DROP COLUMN "numCopies"`);
        await queryRunner.query(`ALTER TABLE "user" ADD "userType" character varying NOT NULL`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "user" DROP COLUMN "userType"`);
        await queryRunner.query(`ALTER TABLE "book" ADD "numCopies" integer NOT NULL`);
    }

}
