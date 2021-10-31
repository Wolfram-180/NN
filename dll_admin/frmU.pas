unit frmU;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, UniProvider, DB, MemDS, DBAccess,
  Uni, MySQLUniProvider;

type
  Tfrm = class(TForm)
    connect1: TUniConnection;
    qUSDJPY: TUniQuery;
    qGBPUSD: TUniQuery;
    qClearGBPUSD: TUniQuery;
    qClearUSDJPY: TUniQuery;
    mysqlnprvdr1: TMySQLUniProvider;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frm: Tfrm;

implementation

{$R *.dfm}

end.
