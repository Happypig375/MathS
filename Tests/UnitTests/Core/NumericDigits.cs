﻿using AngouriMath;
using AngouriMath.Core.Numerix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Core
{
    [TestClass]
    public class NumericDigits
    {
        void Test(Number n, string expected) => Assert.AreEqual(expected, n.ToString());
        static readonly ComplexNumber i = ComplexNumber.ImaginaryOne;
        static readonly ComplexNumber pi = MathS.DecimalConst.pi;
        static readonly ComplexNumber e = MathS.DecimalConst.e;

        // Values are checked with Wolfram Alpha
        // Default precision is 100 digits
        [TestMethod] public void Pi() => Test(pi, "3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117068");
        [TestMethod] public void E() => Test(e, "2.718281828459045235360287471352662497757247093699959574966967627724076630353547594571382178525166427");
        [TestMethod] public void Exp2() => Test(Number.Exp(2), "7.389056098930650227230427460575007813180315570551847324087127822522573796079057763384312485079121795");
        [TestMethod] public void ExpPiI() => Test(Number.Exp(pi * i), "-1");
        [TestMethod] public void SqrtM3() => Test(Number.Sqrt(-3), "1.732050807568877293527446341505872366942805253810380628055806979451933016908800037081146186757248576i");
        [TestMethod] public void SqrtI() => Test(Number.Sqrt(i), "0.7071067811865475244008443621048490392848359376884740365883398689953662392310535194251937671638207864 + 0.7071067811865475244008443621048490392848359376884740365883398689953662392310535194251937671638207862i");
        [TestMethod] public void Sqrt3P2I() => Test(Number.Sqrt(3+2*i), "1.817354021023970620075194486035821926469403643127136112063307705827989943868365693678192017810062678 + 0.5502505227003375110556805653912514370755875596670069658341698224663216572058032360895012802789614795i");
        [TestMethod] public void Sin1() => Test(Number.Sin(1), "0.8414709848078965066525023216302989996225630607983710656727517099919104043912396689486397435430526957");
        [TestMethod] public void SinI() => Test(Number.Sin(i), "1.175201193643801456882381850595600815155717981334095870229565413013307567304323895607117452089623392i");
        [TestMethod] public void SinPi() => Test(Number.Sin(i), "1.175201193643801456882381850595600815155717981334095870229565413013307567304323895607117452089623392i");
        [TestMethod] public void Sin3P2I() => Test(Number.Sin(3+2*i), "0.5309210862485198052670400906606765596727734509514910300870685537180352875306706855293567300083225282 - 3.590564589985779952012565447794816793194913675729301509998621397417882680153461421522759381430149008i");
        [TestMethod] public void Cos1() => Test(Number.Cos(1), "0.5403023058681397174009366074429766037323104206179222276700972553811003947744717645179518560871830896");
        [TestMethod] public void CosI() => Test(Number.Cos(i), "1.543080634815243778477905620757061682601529112365863704737402214710769063049223698964264726435543036");
        [TestMethod] public void Cos3P2I() => Test(Number.Cos(3+2*i), "-3.724545504915322565473970703255972528674965773215330726785894568664950105906529288911014829414174408 - 0.5118225699873846088344638498018756342455566094907438674553837912358533904574111940998404122618726230i");
        [TestMethod] public void Tan1() => Test(Number.Tan(1), "1.557407724654902230506974807458360173087250772381520038383946605698861397151727289555099965202242983");
        [TestMethod] public void TanI() => Test(Number.Tan(i), "0.7615941559557648881194582826047935904127685972579365515968105001219532445766384834589475216736767144i");
        [TestMethod] public void Tan3P2I() => Test(Number.Tan(3+2*i), "-0.009884375038322493720314034303501210979618133534670390318610106061155603556792543443355828521930418933 + 0.9653858790221331242784802693945606858797296500057577736369082400666397728539675500957543613480053582i");
        [TestMethod] public void Cotan1() => Test(Number.Cotan(1), "0.6420926159343307030064199865942656202302781139181713791011622804262768568391646721984829197601968050");
        [TestMethod] public void CotanI() => Test(Number.Cotan(i), "-1.313035285499331303636161246930847832912013941240452655543152967567084270461874382674679241480856303i");
        [TestMethod] public void Cotan3P2I() => Test(Number.Cotan(3+2*i), "-0.0106047834703371017503168962077792923972675909391407246475487930531248300454417108979266267185038252 - 1.035746637764995396112758656897908320248306959923214042664373819742665974520371256149665849444234282i");
        [TestMethod] public void Arcsin1() => Test(Number.Arcsin(1), "1.570796326794896619231321691639751442098584699687552910487472296153908203143104499314017412671058534");
        [TestMethod] public void Arcsin3() => Test(Number.Arcsin(3), "1.570796326794896619231321691639751442098584699687552910487472296153908203143104499314017412671058534 - 1.762747174039086050465218649959584618056320656523270821506591217306754368444052175667413783820512086i");
        [TestMethod] public void Arcsin4() => Test(Number.Arcsin(4), "1.570796326794896619231321691639751442098584699687552910487472296153908203143104499314017412671058534 - 2.063437068895560546727281172620131871456591449883392499836032692765902842847409911780353006403580326i");
        [TestMethod] public void Arcsin3M2I() => Test(Number.Arcsin(3-2*i), "0.9646585044076027920454110594995323555197773725073316527132580297155508786089335572049608301897631770 - 1.968637925793096291788665095245498189520731012682010573842811017352748255492485345887875752070076231i");
        [TestMethod] public void Arcsin3P2I() => Test(Number.Arcsin(3+2*i), "0.9646585044076027920454110594995323555197773725073316527132580297155508786089335572049608301897631770 + 1.968637925793096291788665095245498189520731012682010573842811017352748255492485345887875752070076231i");
        [TestMethod] public void ArcsinI() => Test(Number.Arcsin(i), "0.8813735870195430252326093249797923090281603282616354107532956086533771842220260878337068919102560430i");
        [TestMethod] public void Arccos1() => Test(Number.Arccos(1), "0");
        [TestMethod] public void ArccosI() => Test(Number.Arccos(i), "1.570796326794896619231321691639751442098584699687552910487472296153908203143104499314017412671058534 - 0.8813735870195430252326093249797923090281603282616354107532956086533771842220260878337068919102560430i");
        [TestMethod] public void Arccos3P2I() => Test(Number.Arccos(3+2*i), "0.6061378223872938271859106321402190865788073271802212577742142664383573245341709421090565824812953570 - 1.968637925793096291788665095245498189520731012682010573842811017352748255492485345887875752070076231i");
        [TestMethod] public void Arctan1() => Test(Number.Arctan(1), "0.7853981633974483096156608458198757210492923498437764552437361480769541015715522496570087063355292670");
        [TestMethod] public void ArctanI() => Test(Number.Arctan(i), "+ooi");
        [TestMethod] public void Arctan3P2I() => Test(Number.Arctan(3+2*i), "1.338972522294493561124193575909144241084316172544492778582005751793809271060233646663717270678614588 + 0.1469466662255297520474327851547159424423449403442452953891851939502023996823900422792744078835711420i");
        [TestMethod] public void Arccotan1() => Test(Number.Arccotan(1), "0.7853981633974483096156608458198757210492923498437764552437361480769541015715522496570087063355292670");
        [TestMethod] public void ArccotanI() => Test(Number.Arccotan(i), "-ooi");
        [TestMethod] public void Arccotan3P2I() => Test(Number.Arccotan(3+2*i), "0.2318238045004030581071281157306072010142685271430601319054665443600989320828708526503001419924439467 - 0.1469466662255297520474327851547159424423449403442452953891851939502023996823900422792744078835711419i");
        [TestMethod] public void Log10_1() => Test(Number.Log(10, 1), "0");
        [TestMethod] public void Log10_0() => Test(Number.Log(10, 0), "-oo");
        [TestMethod] public void LogI_I() => Test(Number.Log(i, i), "1");
        [TestMethod] public void LogI_1() => Test(Number.Log(i, 1), "0");
        [TestMethod] public void LogI_M1() => Test(Number.Log(i, -1), "2");
        [TestMethod] public void LogI_2() => Test(Number.Log(i, 2), "-0.4412712003053031867929128642359953819653794974593109409785264674142435340933733649959862237079351117i");
        [TestMethod] public void LogM2_M2() => Test(Number.Log(-2, -2), "1");
        [TestMethod] public void LogM2_0D5() => Test(Number.Log(-2, 0.5m), "-0.04642032354540812805739794967614531473517486433229878976878405934128995144281958985056393738882100657 + 0.2103936242079302074624500066187461838994455574074656160548588507113367960711277189504016452320136900i");
        [TestMethod] public void LogM2_0D4() => Test(Number.Log(-2, 0.4m), "-0.06136432986843633646517722130553639639897513966073574085079108126276417408725123183789917075670358045 + 0.2781252428256368191899557377147340479778729417292667461789200592366343982454240662260594052903331773i");
        [TestMethod] public void LogM2_3P2I() => Test(Number.Log(-2, 3+2*i), "0.2643664916612894989375747779599512529839212984780690192522150852389360266747092012500969179019942120 - 0.3498957094724448147885913933302844456684320929581102596014846703961864239635800903604529923905776442i");
        [TestMethod] public void LnE() => Test(Number.Ln(e), "1");
        [TestMethod] public void LnPi() => Test(Number.Ln(pi), "1.144729885849400174143427351353058711647294812915311571513623071472137769884826079783623270275489708");
        [TestMethod] public void Pow2_3() => Test(Number.Pow(2, 3), "8");
        [TestMethod] public void Pow2_I() => Test(Number.Pow(2, i), "0.7692389013639721265783299936612707014408959949119638531698715074290813468073407890597897424260168071 + 0.6389612763136348011500329114647017842572305378305797294955869566463245224485447499034456609322567387i");
        [TestMethod] public void PowI_2() => Test(Number.Pow(i, 2), "-1");
        [TestMethod] public void PowI_I() => Test(Number.Pow(i, i), "0.2078795763507619085469556198349787700338778416317696080751358830554198772854821397886002778654260353");
        [TestMethod] public void Pow3P2I_3M2I() => Test(Number.Pow(3+2*i,3-2*i), "105.7489754858859776057429151808913270583911891854438642007164406579203854865458413612560669739008053 + 109.0885464095191985068230453514141449073149001480712525014939233398460315014292268679635166428957469i");
        [TestMethod] public void Pow3P2I_3P2I() => Test(Number.Pow(3+2*i,3+2*i), "-5.409738793917671859317094187110414923431153201422932321801121451217085143160320149482544245235856305 - 13.41044237041274830872806422189982707487514435448700312177401211574208044465073698802176970443887704i");
        [TestMethod] public void Pow3M2I_3M2I() => Test(Number.Pow(3-2*i,3-2*i), "-5.409738793917671859317094187110414923431153201422932321801121451217085143160320149482544245235856305 + 13.41044237041274830872806422189982707487514435448700312177401211574208044465073698802176970443887704i");
        [TestMethod] public void Pow3M2I_3P2I() => Test(Number.Pow(3-2*i,3+2*i), "105.7489754858859776057429151808913270583911891854438642007164406579203854865458413612560669739008053 + 109.0885464095191985068230453514141449073149001480712525014939233398460315014292268679635166428957469i");
    }
}
