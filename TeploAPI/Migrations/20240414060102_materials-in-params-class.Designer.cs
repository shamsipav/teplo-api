﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TeploAPI.Data;

#nullable disable

namespace TeploAPI.Migrations
{
    [DbContext(typeof(TeploDBContext))]
    [Migration("20240414060102_materials-in-params-class")]
    partial class materialsinparamsclass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SweetAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastLoginIp")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeploAPI.Models.CokeCunsumptionReference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("IncreaseBlastHumidity")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseGasPressureUnderGrate")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseMassFractionOfManganeseInChugun")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseMassFractionOfPhosphorusInChugun")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseMassFractionOfTitanInChugun")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseNaturalGasCunsimption")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseVolumeFractionOxygenInBlast")
                        .HasColumnType("double precision");

                    b.Property<double>("IronMassFractionIncreaseInOreRash")
                        .HasColumnType("double precision");

                    b.Property<double>("OutputFromLimestoneCharge")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionAshInCokeInRangeOf11to12Percent")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionAshInCokeInRangeOf12to13Percent")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionOfSera")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionOfSeraInChugun")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionOfSiliciumInChugun")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionTrifles")
                        .HasColumnType("double precision");

                    b.Property<double>("ShareCrudeOreReductionCharge")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureIncreaseInRangeOf1001to1100")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureIncreaseInRangeOf1101to1200")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureIncreaseInRangeOf800to900")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureIncreaseInRangeOf901to1000")
                        .HasColumnType("double precision");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("CokeCunsumptionReferences");
                });

            modelBuilder.Entity("TeploAPI.Models.Furnace.Furnace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("DiameterOfColoshnik")
                        .HasColumnType("double precision");

                    b.Property<double>("DiameterOfHorn")
                        .HasColumnType("double precision");

                    b.Property<double>("DiameterOfRaspar")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfColoshnik")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfHorn")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfRaspar")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfShaft")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfTuyeres")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfZaplechiks")
                        .HasColumnType("double precision");

                    b.Property<int>("NumberOfFurnace")
                        .HasColumnType("integer");

                    b.Property<double>("UsefulHeightOfFurnace")
                        .HasColumnType("double precision");

                    b.Property<double>("UsefulVolumeOfFurnace")
                        .HasColumnType("double precision");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Furnaces");
                });

            modelBuilder.Entity("TeploAPI.Models.Furnace.FurnaceBaseParam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("AcceptedTemperatureOfBackupZone")
                        .HasColumnType("double precision");

                    b.Property<double>("AshContentInCoke")
                        .HasColumnType("double precision");

                    b.Property<double>("AverageSizeOfPieceCharge")
                        .HasColumnType("double precision");

                    b.Property<double>("BlastConsumption")
                        .HasColumnType("double precision");

                    b.Property<double>("BlastHumidity")
                        .HasColumnType("double precision");

                    b.Property<double>("BlastPressure")
                        .HasColumnType("double precision");

                    b.Property<double>("BlastTemperature")
                        .HasColumnType("double precision");

                    b.Property<double>("Chugun_C")
                        .HasColumnType("double precision");

                    b.Property<double>("Chugun_MN")
                        .HasColumnType("double precision");

                    b.Property<double>("Chugun_P")
                        .HasColumnType("double precision");

                    b.Property<double>("Chugun_S")
                        .HasColumnType("double precision");

                    b.Property<double>("Chugun_SI")
                        .HasColumnType("double precision");

                    b.Property<double>("ColoshGasPressure")
                        .HasColumnType("double precision");

                    b.Property<double>("ColoshGasTemperature")
                        .HasColumnType("double precision");

                    b.Property<double>("ColoshGas_CO")
                        .HasColumnType("double precision");

                    b.Property<double>("ColoshGas_CO2")
                        .HasColumnType("double precision");

                    b.Property<double>("ColoshGas_H2")
                        .HasColumnType("double precision");

                    b.Property<double>("DailyСapacityOfFurnace")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Day")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("DiameterOfColoshnik")
                        .HasColumnType("double precision");

                    b.Property<double>("DiameterOfHorn")
                        .HasColumnType("double precision");

                    b.Property<double>("DiameterOfRaspar")
                        .HasColumnType("double precision");

                    b.Property<double>("EstablishedLevelOfEmbankment")
                        .HasColumnType("double precision");

                    b.Property<Guid>("FurnaceId")
                        .HasColumnType("uuid");

                    b.Property<double>("HeatCapacityOfAgglomerate")
                        .HasColumnType("double precision");

                    b.Property<double>("HeatCapacityOfCoke")
                        .HasColumnType("double precision");

                    b.Property<double>("HeatCapacityOfPellets")
                        .HasColumnType("double precision");

                    b.Property<double>("HeatOfBurningOfNaturalGasOnFarms")
                        .HasColumnType("double precision");

                    b.Property<double>("HeatOfIncompleteBurningCarbonOfCoke")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfColoshnik")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfHorn")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfRaspar")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfShaft")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfTuyeres")
                        .HasColumnType("double precision");

                    b.Property<double>("HeightOfZaplechiks")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("NaturalGasConsumption")
                        .HasColumnType("double precision");

                    b.Property<int>("NumberOfFurnace")
                        .HasColumnType("integer");

                    b.Property<double>("NumberOfTuyeres")
                        .HasColumnType("double precision");

                    b.Property<double>("OxygenContentInBlast")
                        .HasColumnType("double precision");

                    b.Property<double>("ProportionOfHeatLossesOfLowerPart")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("SaveDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("ShareOfPelletsInCharge")
                        .HasColumnType("double precision");

                    b.Property<double>("SpecificConsumptionOfCoke")
                        .HasColumnType("double precision");

                    b.Property<double>("SpecificConsumptionOfZRM")
                        .HasColumnType("double precision");

                    b.Property<double>("SpecificSlagYield")
                        .HasColumnType("double precision");

                    b.Property<double>("SulfurContentInCoke")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureOfCokeThatCameToTuyeres")
                        .HasColumnType("double precision");

                    b.Property<double>("UsefulHeightOfFurnace")
                        .HasColumnType("double precision");

                    b.Property<double>("UsefulVolumeOfFurnace")
                        .HasColumnType("double precision");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<double>("VolatileContentInCoke")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("FurnacesWorkParams");
                });

            modelBuilder.Entity("TeploAPI.Models.FurnaceCapacityReference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("IncreaseBlastHumidity")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseGasPressureUnderGrate")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseMassFractionOfManganeseInChugun")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseMassFractionOfPhosphorusInChugun")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseMassFractionOfTitanInChugun")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseNaturalGasCunsimption")
                        .HasColumnType("double precision");

                    b.Property<double>("IncreaseVolumeFractionOxygenInBlast")
                        .HasColumnType("double precision");

                    b.Property<double>("IronMassFractionIncreaseInOreRash")
                        .HasColumnType("double precision");

                    b.Property<double>("OutputFromLimestoneCharge")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionAshInCokeInRangeOf11to12Percent")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionAshInCokeInRangeOf12to13Percent")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionOfSera")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionOfSeraInChugun")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionOfSiliciumInChugun")
                        .HasColumnType("double precision");

                    b.Property<double>("ReductionMassFractionTrifles")
                        .HasColumnType("double precision");

                    b.Property<double>("ShareCrudeOreReductionCharge")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureIncreaseInRangeOf1001to1100")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureIncreaseInRangeOf1101to1200")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureIncreaseInRangeOf800to900")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureIncreaseInRangeOf901to1000")
                        .HasColumnType("double precision");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("FurnanceCapacityReferences");
                });

            modelBuilder.Entity("TeploAPI.Models.Material", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Al2O3")
                        .HasColumnType("double precision");

                    b.Property<double>("BaseOne")
                        .HasColumnType("double precision");

                    b.Property<double>("CaO")
                        .HasColumnType("double precision");

                    b.Property<double>("Cr")
                        .HasColumnType("double precision");

                    b.Property<double>("Fe")
                        .HasColumnType("double precision");

                    b.Property<double>("Fe2O3")
                        .HasColumnType("double precision");

                    b.Property<double>("FeO")
                        .HasColumnType("double precision");

                    b.Property<double>("FiveZero")
                        .HasColumnType("double precision");

                    b.Property<double>("MgO")
                        .HasColumnType("double precision");

                    b.Property<double>("Mn")
                        .HasColumnType("double precision");

                    b.Property<double>("MnO")
                        .HasColumnType("double precision");

                    b.Property<double>("Moisture")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("P")
                        .HasColumnType("double precision");

                    b.Property<double>("S")
                        .HasColumnType("double precision");

                    b.Property<double>("SiO2")
                        .HasColumnType("double precision");

                    b.Property<double>("TiO2")
                        .HasColumnType("double precision");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<double>("Zn")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("TeploAPI.Models.MaterialsWorkParams", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Consumption")
                        .HasColumnType("double precision");

                    b.Property<Guid?>("FurnaceBaseParamId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MaterialId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WorkParamsId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FurnaceBaseParamId");

                    b.ToTable("MaterialsWorkParams");
                });

            modelBuilder.Entity("TeploAPI.Models.MaterialsWorkParams", b =>
                {
                    b.HasOne("TeploAPI.Models.Furnace.FurnaceBaseParam", null)
                        .WithMany("MaterialsWorkParamsList")
                        .HasForeignKey("FurnaceBaseParamId");
                });

            modelBuilder.Entity("TeploAPI.Models.Furnace.FurnaceBaseParam", b =>
                {
                    b.Navigation("MaterialsWorkParamsList");
                });
#pragma warning restore 612, 618
        }
    }
}
