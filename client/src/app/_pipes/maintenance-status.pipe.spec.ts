import { MaintenanceStatusPipe } from './maintenance-status.pipe';

describe('MaintenanceStatusPipe', () => {
  it('create an instance', () => {
    const pipe = new MaintenanceStatusPipe();
    expect(pipe).toBeTruthy();
  });
});
