select * from igrac;
select * from lik;
select * from lopov;
select * from rasa;

delete from igrac;

alter table lik
modify IGRAC_ID NUMBER(5) NULL;

alter table RASA
modify LIK_ID number(5) null;

alter table LOPOV
modify LIK_ID number(5) null;

alter table SVESTENIK
modify LIK_ID number(5) null;

alter table CAROBNJAK
modify LIK_ID number(5) null;

alter table OKLOPNIK
modify LIK_ID number(5) null;

alter table BORAC
modify LIK_ID number(5) null;

alter table STRELAC
modify LIK_ID number(5) null;

create or replace TRIGGER "IGRA_AUTO_PK" 
    BEFORE INSERT
    ON IGRA
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT IGRA_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "GROUPMEM_AUTO_PK" 
    BEFORE INSERT
    ON GROUP_MEMBERSHIP
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT GROUPMEM_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "STAZA_AUTO_PK" 
    BEFORE INSERT
    ON STAZA
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT STAZA_AUTO_PK.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "GRUPA_AUTO_PK" 
    BEFORE INSERT
    ON GRUPA
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT GRUPA_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "SRK_AUTO_PK" 
    BEFORE INSERT
    ON STAZA_RESTRICTION_KLASA
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT SRK_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "SRR_AUTO_PK" 
    BEFORE INSERT
    ON STAZA_RESTRICTION_RASA
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT SRR_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "ORK_AUTO_PK" 
    BEFORE INSERT
    ON ORUDJE_RESTRICTION_KLASA
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT ORK_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "ORR_AUTO_PK" 
    BEFORE INSERT
    ON ORUDJE_RESTRICTION_RASA
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT ORR_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "POSEDUJE_AUTO_PK" 
    BEFORE INSERT
    ON POSEDUJE
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT POSEDUJE_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "JEKUPIO_AUTO_PK" 
    BEFORE INSERT
    ON JE_KUPIO
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT JEKUPIO_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "ORUDJE_AUTO_PK" 
    BEFORE INSERT
    ON ORUDJE
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT ORUDJE_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "IGRAC_AUTO_PK" 
    BEFORE INSERT
    ON IGRAC
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT IGRAC_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "BORISE_AUTO_PK" 
    BEFORE INSERT
    ON BORI_SE
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT BORISE_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "POMOCNIK_AUTO_PK" 
    BEFORE INSERT
    ON POMOCNIK
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT POMOCNIK_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "SESIJA_AUTO_PK" 
    BEFORE INSERT
    ON SESIJA
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT SESIJA_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "TEAMMEMB_AUTO_PK" 
    BEFORE INSERT
    ON TEAM_MEMBERSHIP
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT TEAMMEMB_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "TIM_AUTO_PK" 
    BEFORE INSERT
    ON TIM
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT TIM_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "LIK_AUTO_PK" 
    BEFORE INSERT
    ON LIK
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT LIK_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "RASA_AUTO_PK" 
    BEFORE INSERT
    ON RASA
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT RASA_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "LOPOV_AUTO_PK" 
    BEFORE INSERT
    ON LOPOV
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT KLASA_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "SVESTENIK_AUTO_PK" 
    BEFORE INSERT
    ON SVESTENIK
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT KLASA_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "CAROBNJAK_AUTO_PK" 
    BEFORE INSERT
    ON CAROBNJAK
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT KLASA_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "OKLOPNIK_AUTO_PK" 
    BEFORE INSERT
    ON OKLOPNIK
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT KLASA_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "BORAC_AUTO_PK" 
    BEFORE INSERT
    ON BORAC
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT KLASA_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

create or replace TRIGGER "STRELAC_AUTO_PK" 
    BEFORE INSERT
    ON STRELAC
    REFERENCING NEW AS NEW
    FOR EACH ROW
BEGIN
    SELECT KLASA_PK_SEQ.NEXTVAL INTO :NEW.ID FROM DUAL;
END;
/

CREATE TABLE IGRAC (
    ID NUMBER(5) PRIMARY KEY,
    NADIMAK VARCHAR2(15) NOT NULL UNIQUE,
    LOZINKA VARCHAR2(15) NOT NULL,
    POL CHAR(1) DEFAULT 'M' CHECK (POL IN ('M', 'F')),
    IME VARCHAR2(30) NOT NULL,
    PREZIME VARCHAR2(30),
    UZRAST NUMBER(2) CHECK (UZRAST > 13)
);



CREATE TABLE LIK (
    ID NUMBER(5) PRIMARY KEY,
    STEPEN_ZAMORA NUMBER(1) NOT NULL,
    ISKUSTVO NUMBER(9) NOT NULL,
    NIVO_ZDRAVLJA NUMBER(3) CHECK (NIVO_ZDRAVLJA <= 100) NOT NULL,
    ZLATO NUMBER(10) NOT NULL,
    IGRAC_ID NUMBER(5) NOT NULL,
    CONSTRAINT IGRAC_FK_LIK FOREIGN KEY (IGRAC_ID) REFERENCES IGRAC(ID)
);


CREATE TABLE RASA (
    ID NUMBER(5) PRIMARY KEY,
    RASA VARCHAR2(10) NOT NULL,
    TIP_ORUZJA VARCHAR2(10),
    USPESNOST_U_SKRIVANJU NUMBER(3),
    NIVO_ENERGIJE_ZA_MAGIJU NUMBER(3),
    LIK_ID NUMBER(5) NOT NULL,
    CONSTRAINT RASA_FK_LIK FOREIGN KEY (LIK_ID) REFERENCES LIK(ID),
    CONSTRAINT RASA_CHECK_LIK CHECK (RASA IN ('PATULJAK', 'COVEK', 'VILENJAK', 'DEMON', 'ORK'))
);

CREATE TABLE LOPOV (
    ID NUMBER(5) PRIMARY KEY,
    NIVO_ZAMKI NUMBER(3) NOT NULL,
    NIVO_BUKE NUMBER(3) NOT NULL,
    LIK_ID NUMBER(5) NOT NULL,
    CONSTRAINT LIK_FK_LOPOV FOREIGN KEY (LIK_ID) REFERENCES LIK(ID)
);

CREATE TABLE CAROBNJAK (
    ID NUMBER(5) PRIMARY KEY,
    MAGIJE VARCHAR2(100),
    LIK_ID NUMBER(5) NOT NULL,
    CONSTRAINT LIK_FK_CAROBNJAK FOREIGN KEY (LIK_ID) REFERENCES LIK(ID)
);

CREATE TABLE BORAC (
    ID NUMBER(5) PRIMARY KEY,
    KORISTI_STIT NUMBER(1) NOT NULL CHECK (KORISTI_STIT >= 0 AND KORISTI_STIT <= 1),
    DUAL_WIELDER NUMBER(1) NOT NULL CHECK (DUAL_WIELDER >= 0 AND DUAL_WIELDER <= 1),
    LIK_ID NUMBER(5) NOT NULL,
    CONSTRAINT LIK_FK_BORAC FOREIGN KEY (LIK_ID) REFERENCES LIK(ID)
);

CREATE TABLE SVESTENIK (
    ID NUMBER(5) PRIMARY KEY,
    RELIGIJA VARCHAR2(15) NOT NULL,
    BLAGOSLOVI VARCHAR2(100) NOT NULL,
    CAN_HEAL NUMBER(1) NOT NULL CHECK (CAN_HEAL >= 0 AND CAN_HEAL <= 1),
    LIK_ID NUMBER(5) NOT NULL,
    CONSTRAINT LIK_FK_SVESTENIK FOREIGN KEY (LIK_ID) REFERENCES LIK(ID)
);

CREATE TABLE OKLOPNIK (
    ID NUMBER(5) PRIMARY KEY,
    MAX_OKLOP NUMBER(3) NOT NULL,
    LIK_ID NUMBER(5) NOT NULL,
    CONSTRAINT LIK_FK_OKLOPNIK FOREIGN KEY (LIK_ID) REFERENCES LIK(ID)
);

CREATE TABLE STRELAC (
    ID NUMBER(5) PRIMARY KEY,
    LUK_ILI_SAMOSTREL NUMBER(1) NOT NULL CHECK (LUK_ILI_SAMOSTREL >= 0 AND LUK_ILI_SAMOSTREL <= 1),
    LIK_ID NUMBER(5) NOT NULL,
    CONSTRAINT LIK_FK_STRELAC FOREIGN KEY (LIK_ID) REFERENCES LIK(ID)
);

CREATE TABLE SESIJA (
    ID NUMBER(5) PRIMARY KEY,
    ZLATO NUMBER(9) NOT NULL,
    XP NUMBER(9) NOT NULL,
    VREME TIMESTAMP NOT NULL,
    DUZINA NUMBER(6) NOT NULL,
    IGRAC_ID NUMBER(5) NOT NULL,
    CONSTRAINT IGRAC_FK_SESIJA FOREIGN KEY (IGRAC_ID) REFERENCES IGRAC(ID),
    CONSTRAINT SESIJA_UNIQUENESS UNIQUE (VREME, IGRAC_ID)
);

CREATE TABLE POMOCNIK (
    ID NUMBER(5) PRIMARY KEY,
    IME VARCHAR2(15) NOT NULL,
    RASA VARCHAR2(10) CHECK (RASA IN ('PATULJAK', 'COVEK', 'VILENJAK', 'DEMON', 'ORK')) NOT NULL,
    KLASA VARCHAR2(10) CHECK (KLASA IN ('LOPOV', 'SVESTENIK', 'CAROBNJAK', 'OKLOPNIK', 'BORAC', 'STRELAC')) NOT NULL,
    BONUS_ZASTITA NUMBER(3) NOT NULL,
    IGRAC_ID NUMBER(5) NOT NULL,
    CONSTRAINT IGRAC_FK_POMOCNIK FOREIGN KEY (IGRAC_ID) REFERENCES IGRAC(ID),
    CONSTRAINT POMOCNIK_UNIQUENESS UNIQUE (IME, IGRAC_ID)
);

CREATE TABLE TIM (
    ID NUMBER(5) PRIMARY KEY,
    NAZIV VARCHAR2(15) NOT NULL UNIQUE,
    PLASMAN NUMBER(5) NOT NULL UNIQUE,
    MIN_IGRACA NUMBER(3) NOT NULL,
    MAX_IGRACA NUMBER(3) NOT NULL,
    BONUS_XP NUMBER(9) NOT NULL
);

CREATE TABLE GRUPA (
    ID NUMBER(5) PRIMARY KEY
);

CREATE TABLE STAZA (
    ID NUMBER(5) PRIMARY KEY,
    NAZIV VARCHAR2(15) NOT NULL UNIQUE,
    BONUS_XP NUMBER(9) NOT NULL,
    TIMSKA_STAZA NUMBER(1) NOT NULL CHECK (TIMSKA_STAZA >= 0 AND TIMSKA_STAZA <= 1),
    RESTRICTED_STAZA NUMBER(1) NOT NULL CHECK (RESTRICTED_STAZA >= 0 AND RESTRICTED_STAZA <= 1)
);

CREATE TABLE ORUDJE (
    ID NUMBER(5) PRIMARY KEY,
    NAZIV VARCHAR2(50) NOT NULL UNIQUE,
    OPIS VARCHAR2(100) NOT NULL,
    TIP_ORUDJA VARCHAR2(10) NOT NULL CHECK (TIP_ORUDJA IN ('PREDMET', 'ORUZJE', 'OKLOP')),
    ATK_DEF_POENI NUMBER(3) NULL,
    KLJUCNI_PREDMET NUMBER(1) NULL CHECK (KLJUCNI_PREDMET >= 0 AND KLJUCNI_PREDMET <= 1),
    ADDITIONAL_XP NUMBER(9) NULL
);

CREATE TABLE TEAM_MEMBERSHIP(
    ID NUMBER(5) PRIMARY KEY,
    IGRAC_ID NUMBER(5) NOT NULL,
    TIM_ID NUMBER(5) NOT NULL,
    VREME_OD TIMESTAMP NOT NULL,
    CONSTRAINT IGRAC_FK_MEMBERSHIP FOREIGN KEY (IGRAC_ID) REFERENCES IGRAC(ID),
    CONSTRAINT TIM_FK_MEMBERSHIP FOREIGN KEY (TIM_ID) REFERENCES TIM(ID),
    CONSTRAINT TEAM_MEMBERSHIP_UNIQUENESS UNIQUE (IGRAC_ID, TIM_ID, VREME_OD)
);

CREATE TABLE BORI_SE (
    ID NUMBER(5) PRIMARY KEY,
    TIM1_ID NUMBER(5) NOT NULL,
    TIM2_ID NUMBER(5) NOT NULL,
    POBEDNIK_ID NUMBER(5) NOT NULL,
    VREME TIMESTAMP NOT NULL,
    BONUS NUMBER(9) NOT NULL,
    CONSTRAINT IGRAC1_FK FOREIGN KEY (TIM1_ID) REFERENCES TIM(ID),
    CONSTRAINT IGRAC2_FK FOREIGN KEY (TIM2_ID) REFERENCES TIM(ID),
    CONSTRAINT POBEDNIK_FK FOREIGN KEY (POBEDNIK_ID) REFERENCES TIM(ID),
    CONSTRAINT BORI_SE_UNIQUENESS UNIQUE (TIM1_ID, TIM2_ID, VREME)
);

CREATE TABLE GROUP_MEMBERSHIP (
    ID NUMBER(5) PRIMARY KEY,
    IGRAC_ID NUMBER(5) NOT NULL,
    GRUPA_ID NUMBER(5) NOT NULL,
    POBEDJENI_NEPRIJATELJI NUMBER(4) NOT NULL,
    CONSTRAINT IGRAC_FK_GROUP FOREIGN KEY (IGRAC_ID) REFERENCES IGRAC(ID),
    CONSTRAINT GRUPA_FK_GROUP FOREIGN KEY (GRUPA_ID) REFERENCES GRUPA(ID),
    CONSTRAINT GROUP_MEMBERSHIP_UNIQUENESS UNIQUE (IGRAC_ID, GRUPA_ID)
);

CREATE TABLE IGRA (
    ID NUMBER(5) PRIMARY KEY,
    GRUPA_ID NUMBER(5) NOT NULL UNIQUE,
    STAZA_ID NUMBER(5) NOT NULL,
    FINDABLE_ORUDJE_ID NUMBER(5) NULL,
    VREME TIMESTAMP NOT NULL,
    CONSTRAINT STAZA_FK_IGRA FOREIGN KEY (STAZA_ID) REFERENCES STAZA(ID),
    CONSTRAINT GRUPA_FK_IGRA FOREIGN KEY (GRUPA_ID) REFERENCES GRUPA(ID),
    CONSTRAINT FINDABLE_ORUDJE_FK FOREIGN KEY (FINDABLE_ORUDJE_ID) REFERENCES ORUDJE(ID),
    CONSTRAINT IGRA_ID_UNIQUENESS UNIQUE (GRUPA_ID, STAZA_ID, VREME)
);

CREATE TABLE POSEDUJE (
    ID NUMBER(5) PRIMARY KEY,
    IGRAC_ID NUMBER(5) NOT NULL,
    KLJUCNI_PREDMET_ID NUMBER(5) NOT NULL,
    CONSTRAINT IGRAC_FK_POSEDUJE FOREIGN KEY (IGRAC_ID) REFERENCES IGRAC(ID),
    CONSTRAINT KLJUCNI_PREDMET_FK FOREIGN KEY (KLJUCNI_PREDMET_ID) REFERENCES ORUDJE(ID)
);

CREATE TABLE JE_KUPIO (
    ID NUMBER(5) PRIMARY KEY,
    IGRAC_ID NUMBER(5) NOT NULL,
    SHOPPABLE_ORUDJE_ID NUMBER(5) NOT NULL,
    CONSTRAINT IGRAC_FK_JE_KUPIO FOREIGN KEY (IGRAC_ID) REFERENCES IGRAC(ID),
    CONSTRAINT SHOPPABLE_ORUDJE_FK FOREIGN KEY (SHOPPABLE_ORUDJE_ID) REFERENCES ORUDJE(ID)
);

CREATE TABLE STAZA_RESTRICTION_KLASA (
    ID NUMBER(5) PRIMARY KEY,
    RESTRICTED_STAZA_ID NUMBER(5) NOT NULL,
    KLASA VARCHAR2(10) CHECK (KLASA IN ('LOPOV', 'SVESTENIK', 'CAROBNJAK', 'OKLOPNIK', 'BORAC', 'STRELAC')) NOT NULL,
    CONSTRAINT RESTRICTED_STAZA_FK_KLASAREST FOREIGN KEY (RESTRICTED_STAZA_ID) REFERENCES STAZA(ID),
    CONSTRAINT UNIQUENESS_KLASAREST UNIQUE (RESTRICTED_STAZA_ID, KLASA)
);

CREATE TABLE STAZA_RESTRICTION_RASA (
    ID NUMBER(5) PRIMARY KEY,
    RESTRICTED_STAZA_ID NUMBER(5) NOT NULL,
    RASA VARCHAR2(10) CHECK (RASA IN ('PATULJAK', 'COVEK', 'VILENJAK', 'DEMON', 'ORK')) NOT NULL,
    CONSTRAINT RESTRICTED_STAZA_FK_RASAREST FOREIGN KEY (RESTRICTED_STAZA_ID) REFERENCES STAZA(ID),
    CONSTRAINT UNIQUENESS_RASAREST UNIQUE (RESTRICTED_STAZA_ID, RASA)
);

CREATE TABLE ORUDJE_RESTRICTION_KLASA (
    ID NUMBER(5) PRIMARY KEY,
    ORUDJE_ID NUMBER(5) NOT NULL,
    KLASA VARCHAR2(10) CHECK (KLASA IN ('LOPOV', 'SVESTENIK', 'CAROBNJAK', 'OKLOPNIK', 'BORAC', 'STRELAC')) NOT NULL,
    CONSTRAINT ORUDJE_FK FOREIGN KEY (ORUDJE_ID) REFERENCES ORUDJE(ID),
    CONSTRAINT UNIQUENESS_ORUDJE_KLASAREST UNIQUE (ORUDJE_ID, KLASA)
);

CREATE TABLE ORUDJE_RESTRICTION_RASA (
    ID NUMBER(5) PRIMARY KEY,
    ORUDJE_ID NUMBER(5) NOT NULL,
    RASA VARCHAR2(10) CHECK (RASA IN ('PATULJAK', 'COVEK', 'VILENJAK', 'DEMON', 'ORK')) NOT NULL,
    CONSTRAINT ORUDJE_FK_RASAREST FOREIGN KEY (ORUDJE_ID) REFERENCES ORUDJE(ID),
    CONSTRAINT UNIQUENESS_ORUDJE_RASAREST UNIQUE (ORUDJE_ID, RASA)
);
