import { MigrationInterface, QueryRunner } from "typeorm";

export class V11719439132948 implements MigrationInterface {
    name = 'V11719439132948'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`CREATE TABLE "author" ("id" SERIAL NOT NULL, "firstName" character varying NOT NULL, "lastName" character varying NOT NULL, CONSTRAINT "PK_5a0e79799d372fe56f2f3fa6871" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "authorship" ("id" SERIAL NOT NULL, "authorId" integer, "bookId" integer, CONSTRAINT "PK_7b0da37b1a4c8da4af6d54edcc2" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "borrow" ("id" SERIAL NOT NULL, "date" TIMESTAMP NOT NULL, "bookId" integer, "userId" integer, CONSTRAINT "PK_dff0c680b9c6fc99f5a20d67a97" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "category" ("id" SERIAL NOT NULL, "name" character varying NOT NULL, CONSTRAINT "PK_9c4e4a89e3674fc9f382d733f03" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "review" ("id" SERIAL NOT NULL, "comment" character varying NOT NULL, "rating" integer NOT NULL, "bookId" integer, "userId" integer, CONSTRAINT "PK_2e4299a343a81574217255c00ca" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "book" ("id" SERIAL NOT NULL, "title" character varying NOT NULL, "isbn" character varying NOT NULL, "numPages" integer NOT NULL, "numCopies" integer NOT NULL, "paperbackLink" character varying NOT NULL, "year" integer NOT NULL, "pdfFile" bytea NOT NULL, "cover" bytea NOT NULL, "uploaderId" integer, CONSTRAINT "PK_a3afef72ec8f80e6e5c310b28a4" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "user" ("id" SERIAL NOT NULL, "email" character varying NOT NULL, "username" character varying NOT NULL, "password" character varying NOT NULL, "firstName" character varying NOT NULL, "lastName" character varying NOT NULL, "avatar" character varying NOT NULL, CONSTRAINT "PK_cace4a159ff9f2512dd42373760" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "book_categories_category" ("bookId" integer NOT NULL, "categoryId" integer NOT NULL, CONSTRAINT "PK_baff6a8cd85658522dd9568a6ba" PRIMARY KEY ("bookId", "categoryId"))`);
        await queryRunner.query(`CREATE INDEX "IDX_3f2c919594cd1b6386240d6d46" ON "book_categories_category" ("bookId") `);
        await queryRunner.query(`CREATE INDEX "IDX_83b564c6e2518a2af3c60ac9da" ON "book_categories_category" ("categoryId") `);
        await queryRunner.query(`CREATE TABLE "user_categories_category" ("userId" integer NOT NULL, "categoryId" integer NOT NULL, CONSTRAINT "PK_5a62c2d9eba0ec02cda365b9ab7" PRIMARY KEY ("userId", "categoryId"))`);
        await queryRunner.query(`CREATE INDEX "IDX_331665e2e7d360bf2b715dfeea" ON "user_categories_category" ("userId") `);
        await queryRunner.query(`CREATE INDEX "IDX_936afd72159ca6d1143ab3d66a" ON "user_categories_category" ("categoryId") `);
        await queryRunner.query(`ALTER TABLE "authorship" ADD CONSTRAINT "FK_e352fae25fe7bb7ef863b65a0fa" FOREIGN KEY ("authorId") REFERENCES "author"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "authorship" ADD CONSTRAINT "FK_e1c7a27f27f13ee78a01346cfe0" FOREIGN KEY ("bookId") REFERENCES "book"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "borrow" ADD CONSTRAINT "FK_f5c8ea379eee06ce1482f20d101" FOREIGN KEY ("bookId") REFERENCES "book"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "borrow" ADD CONSTRAINT "FK_395ef8d1ea4a0ff8f1fa17f67ad" FOREIGN KEY ("userId") REFERENCES "user"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "review" ADD CONSTRAINT "FK_ae1ec2fd91f77b5df325d1c7b4a" FOREIGN KEY ("bookId") REFERENCES "book"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "review" ADD CONSTRAINT "FK_1337f93918c70837d3cea105d39" FOREIGN KEY ("userId") REFERENCES "user"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "book" ADD CONSTRAINT "FK_9a0abeddaae547139c058e426a7" FOREIGN KEY ("uploaderId") REFERENCES "user"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "book_categories_category" ADD CONSTRAINT "FK_3f2c919594cd1b6386240d6d464" FOREIGN KEY ("bookId") REFERENCES "book"("id") ON DELETE CASCADE ON UPDATE CASCADE`);
        await queryRunner.query(`ALTER TABLE "book_categories_category" ADD CONSTRAINT "FK_83b564c6e2518a2af3c60ac9da6" FOREIGN KEY ("categoryId") REFERENCES "category"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "user_categories_category" ADD CONSTRAINT "FK_331665e2e7d360bf2b715dfeea9" FOREIGN KEY ("userId") REFERENCES "user"("id") ON DELETE CASCADE ON UPDATE CASCADE`);
        await queryRunner.query(`ALTER TABLE "user_categories_category" ADD CONSTRAINT "FK_936afd72159ca6d1143ab3d66af" FOREIGN KEY ("categoryId") REFERENCES "category"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "user_categories_category" DROP CONSTRAINT "FK_936afd72159ca6d1143ab3d66af"`);
        await queryRunner.query(`ALTER TABLE "user_categories_category" DROP CONSTRAINT "FK_331665e2e7d360bf2b715dfeea9"`);
        await queryRunner.query(`ALTER TABLE "book_categories_category" DROP CONSTRAINT "FK_83b564c6e2518a2af3c60ac9da6"`);
        await queryRunner.query(`ALTER TABLE "book_categories_category" DROP CONSTRAINT "FK_3f2c919594cd1b6386240d6d464"`);
        await queryRunner.query(`ALTER TABLE "book" DROP CONSTRAINT "FK_9a0abeddaae547139c058e426a7"`);
        await queryRunner.query(`ALTER TABLE "review" DROP CONSTRAINT "FK_1337f93918c70837d3cea105d39"`);
        await queryRunner.query(`ALTER TABLE "review" DROP CONSTRAINT "FK_ae1ec2fd91f77b5df325d1c7b4a"`);
        await queryRunner.query(`ALTER TABLE "borrow" DROP CONSTRAINT "FK_395ef8d1ea4a0ff8f1fa17f67ad"`);
        await queryRunner.query(`ALTER TABLE "borrow" DROP CONSTRAINT "FK_f5c8ea379eee06ce1482f20d101"`);
        await queryRunner.query(`ALTER TABLE "authorship" DROP CONSTRAINT "FK_e1c7a27f27f13ee78a01346cfe0"`);
        await queryRunner.query(`ALTER TABLE "authorship" DROP CONSTRAINT "FK_e352fae25fe7bb7ef863b65a0fa"`);
        await queryRunner.query(`DROP INDEX "public"."IDX_936afd72159ca6d1143ab3d66a"`);
        await queryRunner.query(`DROP INDEX "public"."IDX_331665e2e7d360bf2b715dfeea"`);
        await queryRunner.query(`DROP TABLE "user_categories_category"`);
        await queryRunner.query(`DROP INDEX "public"."IDX_83b564c6e2518a2af3c60ac9da"`);
        await queryRunner.query(`DROP INDEX "public"."IDX_3f2c919594cd1b6386240d6d46"`);
        await queryRunner.query(`DROP TABLE "book_categories_category"`);
        await queryRunner.query(`DROP TABLE "user"`);
        await queryRunner.query(`DROP TABLE "book"`);
        await queryRunner.query(`DROP TABLE "review"`);
        await queryRunner.query(`DROP TABLE "category"`);
        await queryRunner.query(`DROP TABLE "borrow"`);
        await queryRunner.query(`DROP TABLE "authorship"`);
        await queryRunner.query(`DROP TABLE "author"`);
    }

}
