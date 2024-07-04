import { MigrationInterface, QueryRunner } from "typeorm";

export class V41719661183444 implements MigrationInterface {
    name = 'V41719661183444'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "user_categories_category" DROP CONSTRAINT "FK_936afd72159ca6d1143ab3d66af"`);
        await queryRunner.query(`ALTER TABLE "book_categories_category" DROP CONSTRAINT "FK_83b564c6e2518a2af3c60ac9da6"`);
        await queryRunner.query(`ALTER TABLE "user_categories_category" ADD CONSTRAINT "FK_936afd72159ca6d1143ab3d66af" FOREIGN KEY ("categoryId") REFERENCES "category"("id") ON DELETE CASCADE ON UPDATE CASCADE`);
        await queryRunner.query(`ALTER TABLE "book_categories_category" ADD CONSTRAINT "FK_83b564c6e2518a2af3c60ac9da6" FOREIGN KEY ("categoryId") REFERENCES "category"("id") ON DELETE CASCADE ON UPDATE CASCADE`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "book_categories_category" DROP CONSTRAINT "FK_83b564c6e2518a2af3c60ac9da6"`);
        await queryRunner.query(`ALTER TABLE "user_categories_category" DROP CONSTRAINT "FK_936afd72159ca6d1143ab3d66af"`);
        await queryRunner.query(`ALTER TABLE "book_categories_category" ADD CONSTRAINT "FK_83b564c6e2518a2af3c60ac9da6" FOREIGN KEY ("categoryId") REFERENCES "category"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "user_categories_category" ADD CONSTRAINT "FK_936afd72159ca6d1143ab3d66af" FOREIGN KEY ("categoryId") REFERENCES "category"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
    }

}
