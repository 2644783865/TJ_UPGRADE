<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_ZCFZ.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_ZCFZ" Title="�ޱ���ҳ" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �ʲ���ծ�� 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-wrapper">
      <div class="box-inner">
        <div class="box-title">
        <table style="width: 100%;">
                    <tr>
                        <td style="width: 40%;">
                              <strong>ʱ�䣺</strong>
                            <asp:DropDownList ID="dplYear" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp;��&nbsp;
                            <asp:DropDownList ID="dplMoth" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_dplYearMoth"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            &nbsp;��&nbsp;
                         </td>
                         <td>
                         <asp:FileUpload runat="server" ID="FileUpload1" Width="200px" />
                         </td>
                         <td>
                             <asp:Button ID="btn_Import_Click" runat="server" Text="����" 
                                 onclick="btn_Import_Click_Click" />
                         </td>
                         <td>
                            <asp:Button ID="btnexport" runat="server" Text="����" OnClick="btnexport_Click" />
                         </td>
                         <td align="right" style="width: 358px"><asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:window.showModalDialog('FM_ZCFZ_Detail.aspx?FLAG=ADD','','dialogWidth=650px;dialogHeight=400px');" runat="server">
                          <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                             ���</asp:HyperLink>&nbsp;&nbsp;
                         </td>
                         <td align="right">
                             <asp:Button ID="btnSC" runat="server" Text="ɾ��" OnClientClick="javascript:return confirm('ȷ��Ҫɾ����');" onclick="btnSC_Click"  />
                         </td>
                   </tr>
        </table>
        </div>
      </div>
      <div class="box-outer">
      <div style=" overflow:scroll">
         <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                border="1" width="100%">
                <asp:Repeater ID="rptProNumCost" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                            <td colspan="4" style="font-family: ����, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                ѡ��
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="14" style="font-family: ����, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                �����ʲ�
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="19" style="font-family: ����, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                �������ʲ�
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="13" style="font-family: ����, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                ������ծ
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="8" style="font-family: ����, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                ��������ծ
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td colspan="7" style="font-family: ����, Arial, Helvetica, sans-serif; font-size: small; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #000000">
                                ������Ȩ��
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                           
                            
                        </tr>
                        <tr align="center" class="tableTitle headcolor">
                            <td>
                                ɾ��/����
                            </td>
                            <td>
                                ���ڱ��
                            </td>
                            <td>
                                �޸�
                            </td>
                            <td>
                                �鿴
                            </td>
                            <td>
                                ��������
                            </td>
                            <td>
                                �����ʽ�
                            </td>
                            <td>
                                ���н������Ĵ��
                            </td>
                            <td>
                                �����Խ����ʲ�
                            </td>
                            <td>
                                Ӧ��Ʊ��
                            </td>
                            <td>
                                Ӧ���˿�ԭֵ
                            </td>
                            <td>
                                ������׼��
                            </td>
                            <td>
                                Ӧ���˿ֵ
                            </td>
                            <td>
                                Ԥ������
                            </td>
                            <td>
                                Ӧ����Ϣ
                            </td>
                            <td>
                                Ӧ�չ���
                            </td>
                            <td>
                                ����Ӧ�տ�
                            </td>
                            <td>
                                ���
                            </td>
                            <td>
                                һ���ڵ��ڵķ������ʲ�
                            </td>
                            <td>
                                ���������ʲ�
                            </td>
                            <td>
                                �����ʲ��ϼ�
                            </td>
                            <td>
                                �ɹ����۽����ʲ�
                            </td>
                            <td>
                                ����������Ͷ��
                            </td>
                            <td>
                                ����Ӧ�տ�
                            </td>
                            <td>
                                ���ڹ�ȨͶ��
                            </td>
                            <td>
                                Ͷ���Է��ز�
                            </td>
                            <td>
                                �̶��ʲ�ԭֵ
                            </td>
                            <td>
                                ���ۼ��۾�
                            </td>
                            <td>
                                �̶��ʲ���ֵ
                            </td>
                            <td>
                                ���̶��ʲ���ֵ׼��
                            </td>
                            <td>
                                �̶��ʲ�����
                            </td>
                            <td>
                                �ڽ�����
                            </td>
                            <td>
                                ��������
                            </td>
                            <td>
                                �̶��ʲ�����
                            </td>
                            <td>
                                �����ʲ�
                            </td>
                            <td>
                                ����֧��
                            </td>
                            <td>
                                ���� 
                            </td>
                            <td>
                                ���ڴ�̯����
                            </td>
                            <td>
                                ��������˰�ʲ�
                            </td>
                            <td>
                                �����������ʲ�
                            </td>
                            <td>
                                �������ʲ��ϼ�
                            </td>
                            <td>
                                �ʲ��ܼ�
                            </td>
                            <td>
                                ���ڽ��
                            </td>
                            <td>
                                ���н������Ĵ���
                            </td>
                            <td>
                                �����Խ��ڸ�ծ
                            </td>
                            <td>
                                Ӧ��Ʊ��
                            </td>
                            <td>
                                Ӧ���˿�
                            </td>
                            <td>
                                Ԥ�տ���
                            </td>
                            <td>
                                Ӧ��ְ��н��
                            </td>
                            <td>
                                Ӧ��˰��
                            </td>
                            <td>
                                Ӧ����Ϣ
                            </td>
                            <td>
                                Ӧ������
                            </td>
                            <td>
                                ����Ӧ����
                            </td>
                            <td>
                                һ���ڵ��ڵķ�������ծ
                            </td>
                            <td>
                                ����������ծ
                            </td>
                            <td>
                                ������ծ�ϼ�
                            </td>
                            <td>
                                ���ڽ��
                            </td>    
                            <td>
                                ���н������Ĵ���
                            </td>
                            <td>
                                Ӧ��ծȯ
                            </td>
                            <td>
                                ����Ӧ����
                            </td>
                            <td>
                                ר��Ӧ����
                            </td>
                            <td>
                                Ԥ�Ƹ�ծ
                            </td>
                            <td>
                                ��������˰��ծ
                            </td>
                            <td>
                                ������������ծ
                            </td>
                            <td>
                                ��������ծ�ϼ�
                            </td>
                            <td>
                                ��ծ�ϼ�
                            </td>
                            <td>
                                ʵ���ʱ�
                            </td>
                            <td>
                                ���ѹ黹Ͷ��
                            </td>
                            <td>
                                �ʱ�����
                            </td>
                            <td>
                                ����
                            </td>
                            <td>
                                ӯ�๫��
                            </td>
                            <td>
                                ר���
                            </td>
                            <td>
                                δ��������
                            </td>
                            <td>
                                ������Ȩ��ϼ�
                            </td>
                            <td>
                                ��ծ��������Ȩ���ܼ�
                            </td>
                                
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                            <asp:Label ID="lblID" runat="server" visible="false" Text='<%#Eval("ID")%>'></asp:Label>
                            <td>
                                 <asp:CheckBox ID="chkDel" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                 Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                 <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                             </td>
                             <td align="center">
                                <asp:Label ID="RQBH" runat="server" Enabled="false" Text='<%#Eval("RQBH")%>'></asp:Label>
                            </td>
                            <td><asp:HyperLink ID="HyperLinkXG" NavigateUrl='<%# editDq(Eval("RQBH").ToString()) %>'  runat="server" >
                            <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                                �޸�</asp:HyperLink>
                            </td>
                            <td id="zcView" runat="server">
                                <asp:HyperLink ID="HyperLinkCK" NavigateUrl='<%# viewDq(Eval("RQBH").ToString()) %>'  runat="server" >
                                <asp:Image ID="Image3" ImageUrl="~/assets/images/search.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                            </asp:HyperLink>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZCFZ_TYPE" runat="server" Enabled="false" Text='<%#Eval("ZCFZ_TYPE")%>'></asp:Label>
                            </td>
                           <td align="center">
                                <asp:Label ID="ZC_LD_HBZJ" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_HBZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_HBZJ_JS" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_HBZJ_JS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_JYJR" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_JYJR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YSPJ" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YSPJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YSZKYZ" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YSZKYZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_JH" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_JH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YSZKJZ" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YSZKJZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YFKX" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YFKX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YSLX" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YSLX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YSGL" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YSGL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_QTYS" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_QTYS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_CH" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_CH")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_YNFLD" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_YNFLD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_QT" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_LD_HJ" runat="server" Enabled="false" Text='<%#Eval("ZC_LD_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_KJ" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_KJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_CDT" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_CDT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_CQYS" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_CQYS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_CQGQT" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_CQGQT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_TF" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_TF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_GZY" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_GZY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_JL" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_JL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_GZJZ" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_GZJZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_JG" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_JG")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_GZJE" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_GZJE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_ZJ" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_ZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_GCWZ" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_GCWZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_GZQL" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_GZQL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_WXZC" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_WXZC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_KFZC" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_KFZC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_SY" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_SY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_CQDT" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_CQDT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_DYSD" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_DYSD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_QT" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_FLD_HJ" runat="server" Enabled="false" Text='<%#Eval("ZC_FLD_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="ZC_ZJ" runat="server" Enabled="false" Text='<%#Eval("ZC_ZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_DQJK" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_DQJK")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_DQJK_JS" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_DQJK_JS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_JYJR" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_JYJR")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YFPJ" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YFPJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YFZK" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YFZK")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YSKX" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YSKX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YFXC" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YFXC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YJSF" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YJSF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YFLX" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YFLX")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YFGL" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YFGL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_QTYF" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_QTYF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_YNDF" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_YNDF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_QT" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_LD_HJ" runat="server" Enabled="false" Text='<%#Eval("FZ_LD_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_CQJK" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_CQJK")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_CQJK_JS" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_CQJK_JS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_YFZJ" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_YFZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_CQYF" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_CQYF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_ZXYF" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_ZXYF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_YJFZ" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_YJFZ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_DYSD" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_DYSD")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_QT" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_QT")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_FLD_HJ" runat="server" Enabled="false" Text='<%#Eval("FZ_FLD_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZ_HJ" runat="server" Enabled="false" Text='<%#Eval("FZ_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_SSZB" runat="server" Enabled="false" Text='<%#Eval("QY_SSZB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_JY" runat="server" Enabled="false" Text='<%#Eval("QY_JY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_ZBGJ" runat="server" Enabled="false" Text='<%#Eval("QY_ZBGJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_JK" runat="server" Enabled="false" Text='<%#Eval("QY_JK")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_YYGJ" runat="server" Enabled="false" Text='<%#Eval("QY_YYGJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_ZXCB" runat="server" Enabled="false" Text='<%#Eval("QY_ZXCB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_WFP" runat="server" Enabled="false" Text='<%#Eval("QY_WFP")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="QY_HJ" runat="server" Enabled="false" Text='<%#Eval("QY_HJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="FZQY_ZJ" runat="server" Enabled="false" Text='<%#Eval("FZQY_ZJ")%>'></asp:Label>
                            </td>                   
                             
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                û�м�¼!<br />
                <br />
            </asp:Panel>
            <uc1:ucpaging ID="UCPaging1" runat="server" />
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Label ID="LabelDate" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>    
        </div>
      </div>
</asp:Content>
